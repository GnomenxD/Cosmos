﻿using Cosmos.Collections;
using CosmosFramework.CoreModule;
using CosmosFramework.PhysicsModule;

namespace CosmosFramework
{
	/// <summary>
	/// Any <see cref="CosmosFramework.GameObject"/> that must interact with other objects through colliders, both objects must have a <see cref="CosmosFramework.Rigidbody"/> component attached to them. This will trigger events like OnTriggerEnter(Collider), OnTriggerExit(Collider) and OnCollision(Collision) attached to all components on the <see cref="CosmosFramework.GameObject"/> if they're enabled. Colliders without a <see cref="CosmosFramework.Rigidbody"/> component attached will still be collected when using <see cref="CosmosFramework.Physics"/>.
	/// </summary>
	public abstract class Collider : Component, IPhysicsComponent
	{
		// TO DO: Move collision functionality to Rigidbody Component instead
		//Since we allow for multiple Colliders on a single GameObject they will each trigger an event
		//
		protected static Colour triggerColour = new Colour(52, 171, 255);
		protected static Colour activeColour = new Colour(52, 255, 52);
		protected static Colour collisionColour = new Colour(255, 171, 52);
		public static bool DebugVisualise { get; set; } = true;

		private readonly DirtyList<Collider> observedColliders = new DirtyList<Collider>();
		private bool isTriggerOnly;
		private bool visualiseBounds;
		private bool isRigidbodyCollider;

		private bool IsColliding => observedColliders.Count > 0;
		/// <summary>
		/// The collider's position. Can override to fit the specific collider.
		/// </summary>
		internal virtual Vector2 Position => Transform.Position;
		public bool IsTriggerOnly { get => isTriggerOnly; set => isTriggerOnly = value; }
		internal bool IsRigidbodyCollider { get => isRigidbodyCollider && !IsTriggerOnly; set => isRigidbodyCollider = value; }
		/// <summary>
		/// 
		/// </summary>
		public bool Visualise { get => visualiseBounds; set => visualiseBounds = value; }
		/// <summary>
		/// The visualisation colour of the Collider.
		/// </summary>
		protected Colour CollisionColour => IsColliding ? collisionColour : (IsTriggerOnly ? triggerColour : activeColour);

		/// <summary>
		/// Checks the collision between <see langword="this"/> and another <see cref="CosmosFramework.Collider"/>, will return <see langword="true"/> if they intersect. Will also invoke methods like OnTriggerEnter and OnCollision on both game objects and all their components. Collision will only invoke if the <see cref="CosmosFramework.Collider.IsTriggerOnly"/> is <see langword="false"/> and the <see cref="CosmosFramework.GameObject"/> has a <see cref="CosmosFramework.Rigidbody"/> component attached that is not kinematic.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool CheckCollision(Collider other) => Collider.CalculateCollisionIntersection(this, other);

		/// <summary>
		/// Invokes collision on <see langword="this"/> <see cref="CosmosFramework.Collider"/>, this will invoke methods like OnTriggerEnter and OnCollision on its <see cref="CosmosFramework.GameObject"/> and all attached components as long as the <paramref name="other"/> is not already on <see langword="this"/> observed list. OnTriggerExit will automatically be invoked once the two colliders are no longer intersecting.
		/// </summary>
		/// <param name="other"></param>
		private void InvokeCollision(Collider other)
		{
			if (other == null || other.Destroyed || !other.Enabled)
				return;

			if(!observedColliders.Contains(other))
			{
				observedColliders.Add(other);
				observedColliders.IsDirty = true;

				//GameObject.OnTriggerEnter(other);
				if (IsRigidbodyCollider && other.IsRigidbodyCollider)
				{
					//GameObject.OnCollision(new Collision());
				}
			}
		}

		private void RevokeCollision(Collider other)
		{
			if(observedColliders.Contains(other))
			{
				//GameObject.OnTriggerExit(other);
			}
		}

		protected override void Awake()
		{
			Transform.TransformUpdateEvent += RecalculateBounds;
		}

		protected override void OnDestroy()
		{
			Transform.TransformUpdateEvent -= RecalculateBounds;
		}

		protected override void Start()
		{
			if (!GetComponent<Rigidbody>())
				IsTriggerOnly = true;
		}

		protected override void OnEnable()
		{
			RecalculateBounds();
		}

		protected override void Update()
		{
			for(int i = 0; i < observedColliders.Count; i++)
			{
				Collider other = observedColliders[i];
				//Debug.FastLog($"{Name} --- Observed Colliders: {other}");
				if (other.Destroyed || !other.Enabled || !CalculateCollisionIntersection(this, other))
				{
					RevokeCollision(other);
					observedColliders.IsDirty = true;
					observedColliders[i] = null;
				}
			}
			if(observedColliders.IsDirty)
			{
				observedColliders.RemoveAll(item => item == null);
			}

			Gizmos.DrawBox(Vector2.Zero, Vector2.Zero);
		}

		protected override void OnDrawGizmos()
		{
#if EDITOR
			DrawCollisionDebug();
#endif
		}

		/// <summary>
		/// Checks the collision between <paramref name="colliderA"/> and <paramref name="colliderB"/>, will return <see langword="true"/> if they intersect. Will also invoke methods like OnTriggerEnter and OnCollision on both game objects and all their components. Collision will only invoke if the <see cref="CosmosFramework.Collider.IsTriggerOnly"/> is <see langword="false"/> and the <see cref="CosmosFramework.GameObject"/> has a <see cref="CosmosFramework.Rigidbody"/> component attached that is not kinematic.
		/// </summary>
		/// <param name="colliderA"></param>
		/// <param name="colliderB"></param>
		/// <returns></returns>
		public static bool CalculateCollisionIntersection(Collider colliderA, Collider colliderB)
		{
			stopwatch.Restart();
			if (colliderA == null || colliderA.Destroyed || !colliderA.Enabled ||
				colliderB == null || colliderB.Destroyed || !colliderB.Enabled)
			{
				return false;
			}
			bool collision = PhysicsIntersection.GetCollision(colliderA, colliderB);
			if(collision)
			{
				colliderA.InvokeCollision(colliderB);
				colliderB.InvokeCollision(colliderA);
			}
			elapsedTime += stopwatch.Elapsed.TotalMilliseconds;
			return collision;
		}
		/// <summary>
		/// Invoked when the <see cref="CosmosFramework.Transform"/> changes position, can be used to recalculate and cache values instead of calculations every frame.
		/// </summary>
		protected abstract void RecalculateBounds();
		protected abstract void DrawCollisionDebug();

		private static System.Diagnostics.Stopwatch stopwatch { get; set; } = new System.Diagnostics.Stopwatch();
		public static double elapsedTime = 0d;
	}
}