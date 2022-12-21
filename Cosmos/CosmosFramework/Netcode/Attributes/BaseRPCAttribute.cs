﻿using System;

namespace CosmosFramework.Netcode
{
	public abstract class BaseRPCAttribute : Attribute
	{

	}

	/// <summary>
	/// A <see cref="CosmosFramework.Netcode.ServerRPCAttribute"/> provides the ability to send information from a client to the server, as if you would invoke a method from within a class. To invoke the RPC a client must have authority on the <see cref="CosmosFramework.Netcode.NetcodeObject"/>.
	/// <para>Remote procedure call (RPC) will always be received and executed. Unless channel is set to <see cref="CosmosFramework.Netcode.Channel.Unreliable"/>.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class ServerRPCAttribute : BaseRPCAttribute
	{
		public Channel channel = Channel.Reliable;
		public bool ignoreAuthority = false;
	}

	/// <summary>
	/// A <see cref="CosmosFramework.Netcode.ClientRPCAttribute"/> provides the ability for the server to invoke a method within a class on clients.
	/// <para>Remote procedure call (RPC) will always be received and executed. Unless channel is set to <see cref="CosmosFramework.Netcode.Channel.Unreliable"/>.</para>
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class ClientRPCAttribute : Attribute
	{
		public Channel channel = Channel.Reliable;
		public bool excludeOwner = false;
	}
}