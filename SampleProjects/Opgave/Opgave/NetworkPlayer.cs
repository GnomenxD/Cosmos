using CosmosFramework;
using CosmosFramework.InputModule;
using CosmosFramework.Netcode;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Opgave
{
	internal class NetworkPlayer : GameBehaviour
	{

		private NetcodeVariable<string> playerName = new NetcodeVariable<string>("hello there");

		private float currentHealth;
		private float currentSpeed;

		private NetcodeVariable<int> value = new NetcodeVariable<int>(100);
		private NetcodeVariable<float> output = new NetcodeVariable<float>(100.0f);

		private float damageMultipler;
		private int sellPrice;

		private float damageMultipler1;
		private int sellPrice1;
		private float damageMultipler2;
		private int sellPrice2;
		private float damageMultipler3;
		private int sellPrice3;
		private float damageMultipler4;
		private int sellPrice4;
		private float damageMultipler5;
		private int sellPrice5;
		private float damageMultipler6;
		private int sellPrice6;
		private float damageMultipler7;
		private int sellPrice7;
		private float damageMultipler8;
		private int sellPrice8;
		private float damageMultipler9;
		private int sellPrice9;

		private bool isMine1;
		private bool isMine2;
		private bool isMine3;
		private bool isMine4;
		private bool isMine5;
		private bool isMine6;
		private bool isMine7;
		private bool isMine8;
		private bool isMine9;

		private NetcodeVariable<Vector2> netPosition = new NetcodeVariable<Vector2>(Vector2.Half);
		private NetcodeVariable<float> netRotation = new NetcodeVariable<float>(10.0f);

		private NetcodeVariable<Vector2> netPosition1 = new NetcodeVariable<Vector2>(Vector2.Up);
		private NetcodeVariable<float> netRotation1 = new NetcodeVariable<float>(20.0f);

		private NetcodeVariable<Vector2> netPosition2 = new NetcodeVariable<Vector2>(Vector2.Right);
		private NetcodeVariable<float> netRotation2 = new NetcodeVariable<float>(30.0f);

		private NetcodeVariable<Vector2> netPosition3 = new NetcodeVariable<Vector2>(Vector2.Down);
		private NetcodeVariable<float> netRotation3 = new NetcodeVariable<float>(40.0f);

		private NetcodeVariable<Vector2> netPosition4 = new NetcodeVariable<Vector2>(Vector2.Left);
		private NetcodeVariable<float> netRotation4 = new NetcodeVariable<float>(50.0f);

		private bool isMine;

		private List<NetVar> netcodeVariables = new List<CosmosFramework.Netcode.NetVar>();

		protected override void Start()
		{
			var fieldInfos = GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
			Flag operation = new Flag();
			int netvarIndex = 0;
			for(int i = 0; i < fieldInfos.Length; i++)
			{
				FieldInfo field = fieldInfos[i];
				if(field.FieldType.IsAssignableTo(typeof(NetVar)))
				{
					//Debug.Log($"[{field.FieldType}] {field.Name} Mark flag {i}");
					operation.Mark(netvarIndex++);
					netcodeVariables.Add((NetVar)field.GetValue(this));
					Debug.Log($"Field: {(NetVar)field.GetValue(this)}");
				}
				else
				{
					//Debug.Log($"[{field.FieldType}] {field.Name} Not Marked");
				}
			}

			Debug.Log($"{operation.ToByteString()} | {operation.ToString()}");

			string markedFlags = string.Empty;
			fieldInfos = GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
			foreach (int flag in operation.Iterate())
			{
				markedFlags += $"[{flag}] | ";
			}

			string serializedMessage = JsonConvert.SerializeObject(operation.PackedValue);
			byte[] jsonData = Encoding.UTF8.GetBytes(serializedMessage);

			Debug.Log($"Flags: {markedFlags} {{{jsonData.Length}}}");
			input.Begin();
		}

		private KeyboardInput input = new KeyboardInput(InputRestrictions.OnlyNumbers);

		protected override void Update()
		{
			Debug.QuickLog($"Input: {input}");
			if (InputManager.GetKeyDown(Keys.Enter))
			{
				if (int.TryParse(input, out int result))
				{
					if (result < netcodeVariables.Count)
					{
						NetVar netcodeVar = netcodeVariables[result];
						Debug.QuickLog(netcodeVar);
						object value = netcodeVar.Read();
						Debug.Log($"Marked as dirty: {result} | Value: {value} {{{value.GetType()}}}");
						netcodeVariables[result].IsDirty = true;
					}
					else
					{
						Debug.Log($"Out of bounds");
					}
				}
				input.Clear();
			}

			if(InputManager.GetKeyDown(Keys.Space))
			{
				Flag dirtyMarks = new Flag();
				for (int i = 0; i < netcodeVariables.Count; i++)
				{
					NetVar var = netcodeVariables[i];
					if (var.IsDirty)
					{
						dirtyMarks.Mark(i);
						var.IsDirty = false;
					}
				}
				string markedFlags = string.Empty;
				foreach (int flag in dirtyMarks.Iterate())
				{
					markedFlags += $"[{flag}] | ";
				}

				string serializedMessage = JsonConvert.SerializeObject(dirtyMarks.PackedValue);
				byte[] jsonData = Encoding.UTF8.GetBytes(serializedMessage);
				Debug.Log($"Flags: {markedFlags}{dirtyMarks.PackedValue} {{{jsonData.Length}}}");
			}

		}

		private void ValueChanged(int preValue, int newValue)
		{

		}
	}
}