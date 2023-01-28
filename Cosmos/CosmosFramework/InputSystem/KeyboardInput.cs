using CosmosFramework.Collections;
using CosmosFramework.CoreModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using TextInputEventArgs = Microsoft.Xna.Framework.TextInputEventArgs;

namespace CosmosFramework.InputModule
{
	public class KeyboardInput : Resource
	{
		private static readonly DirtyList<KeyboardInput> keyboardInputHandlers = new DirtyList<KeyboardInput>();

		private readonly Stack<char> lastDeletedCharacter;
		private StringBuilder stringBuilder;
		private bool enabled;
		private bool allowLineEnding;
		private int line;
		private InputRestrictions restrictions;

		public InputRestrictions Restrictions { get => restrictions; set => restrictions = value; }
		public bool Enabled => enabled;

		public KeyboardInput(InputRestrictions restrictions = InputRestrictions.None)
		{
			this.restrictions = restrictions;
			stringBuilder = new StringBuilder();
			lastDeletedCharacter = new Stack<char>();
			keyboardInputHandlers.Add(this);
		}

		public void Begin()
		{
			enabled = true;
		}

		public void End()
		{
			enabled = false;
		}

		public void Clear()
		{
			lastDeletedCharacter.Clear();
			stringBuilder.Clear();
		}

		internal void TextInput(TextInputEventArgs input)
		{
			if (!enabled)
				return;

			if(InputState.Held(Keys.LeftControl) || InputState.Held(Keys.RightControl))
			{
				//Does not work when using MTA programs... ignore for now 
				goto clipboardSkip;
				Debug.QuickLog("Control Held");
				//if (input.Key.Convert() == Keys.V)
				//{
				//	//Paste text from clipboard to input.
				//	string clipboard = Clipboard.GetText();
				//	stringBuilder.Append(clipboard);
				//}
				//else if (input.Key.Convert() == Keys.C)
				//{
				//	//Copy text from input to clipboard.
				//	if (!string.IsNullOrWhiteSpace(stringBuilder.ToString()))
				//	{
				//		Clipboard.SetText(stringBuilder.ToString());
				//	}
				//}
				//return;
			}
			clipboardSkip:

			if (input.Key.Convert() == Keys.Back)
			{
				if (stringBuilder.Length > 0)
				{
					lastDeletedCharacter.Push(stringBuilder[stringBuilder.Length - 1]);
					stringBuilder.Length--;
				}
				return;
			}
			if(input.Key.Convert() == Keys.Delete)
			{
				return;
			}

			if(input.Key.Convert() == Keys.Tab)
			{
				stringBuilder.Append($"\t");
				return;
			}
			if (input.Key.Convert() == Keys.Enter)
			{
				if (!allowLineEnding)
					return;
				stringBuilder.Append($"\n");
				return;
			}
			
			if(Restrictions != InputRestrictions.None)
			{
				if(int.TryParse(input.Character.ToString(), out _))
				{
					if (Restrictions == InputRestrictions.OnlyCharacters)
						return;
				}
				else
				{
					if (Restrictions == InputRestrictions.OnlyNumbers)
						return;
				}
			}
			stringBuilder.Append(input.Character);
		}

		internal static void InputHandler(object sender, TextInputEventArgs e)
		{
			foreach(KeyboardInput input in keyboardInputHandlers)
			{
				if(input.IsDisposed)
				{
					keyboardInputHandlers.IsDirty = true;
					continue;
				}

				input.TextInput(e);
			}
			keyboardInputHandlers.DisposeAll(item => item.IsDisposed);
		}

		protected override void Dispose(bool disposing)
		{
			if (!IsDisposed && disposing)
			{
				stringBuilder = null;
				enabled = false;
			}
			base.Dispose(disposing);
		}

		public string Read()
		{
			string input = stringBuilder.ToString();
			lastDeletedCharacter.Clear();
			stringBuilder.Clear();
			return input;
		}
		public override string ToString() => stringBuilder.ToString();

		public static implicit operator string(KeyboardInput input) => input.ToString();
	}
}