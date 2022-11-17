using CosmosEngine.Collections;
using CosmosEngine.CoreModule;
using System.Collections.Generic;
using System.Text;
using TextInputEventArgs = Microsoft.Xna.Framework.TextInputEventArgs;

namespace CosmosEngine.InputModule
{
	public class KeyboardInput : Resource
	{
		private static readonly DirtyList<KeyboardInput> keyboardInputHandlers = new DirtyList<KeyboardInput>();

		private readonly Stack<char> lastDeletedCharacter;
		private StringBuilder stringBuilder;
		private bool enabled;
		private bool allowLineEnding;
		private int line;

		public bool Enabled { get => enabled; set => enabled = value; }

		public KeyboardInput()
		{
			enabled = true;
			stringBuilder = new StringBuilder();
			lastDeletedCharacter = new Stack<char>();
			keyboardInputHandlers.Add(this);
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
				stringBuilder.Append($"\n");
				return;
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

		public override string ToString() => stringBuilder.ToString();
	}
}