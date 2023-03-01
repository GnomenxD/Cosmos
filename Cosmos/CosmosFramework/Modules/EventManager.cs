﻿using Cosmos.Collections;
using CosmosFramework.EventSystems;
using CosmosFramework.EventSystems.Base;
using System;
using System.Collections.Generic;

namespace CosmosFramework.Modules
{
	public sealed class EventManager : ObserverManager<IEventHandler, EventManager>
	{
		private readonly List<IPointerHandler> registreretPointerHandlers = new List<IPointerHandler>();
		private readonly DirtyList<Observer> registeredObservers = new DirtyList<Observer>();

		public override void Initialize()
		{
			base.Initialize();
			ObjectDelegater.CreateNewDelegation<IEventHandler>(SubscribeItem);
			ObjectDelegater.CreateNewDelegation<Observer>(CreateNewObserver);
		}

		private void CreateNewObserver(Observer observer)
		{
			registeredObservers.Add(observer);
		}

		public override void BeginEventCall()
		{
			foreach(Observer observer in registeredObservers)
			{
				if(observer.TryInvoke())
				{
					observer.Delta -= Time.DeltaTime;
				}
				else
				{
					registeredObservers.IsDirty = true;
				}
			}
			registeredObservers.DisposeAll((i) => !i.Alive);

			Pointer.IsOverObject = false;
			foreach(IEventHandler handler in observerList)
			{
				if(handler.Destroyed)
				{
					observerList.IsDirty = true;
					continue;
				}
				if (!handler.Enabled)
					continue;

				if(handler is IPointerHandler pointerHandler)
				{
					PointerEventData pointerData = new PointerEventData();
					bool overlap = IsOverPointerHandler(pointerHandler);

					if(overlap)
					{
						if(!registreretPointerHandlers.Contains(pointerHandler))
						{
							registreretPointerHandlers.Add(pointerHandler);
							if (handler is IPointerEnter pointerEnter)
							{
								pointerEnter.OnPointerEnter(pointerData);
							}
						}

						if(handler is IPointerClick pointerClick)
						{
							if (InputState.Pressed(InputModule.MouseButton.Left) || InputState.Pressed(InputModule.MouseButton.Right))
								pointerClick.OnPointerClick(pointerData);
						}
						if (handler is IPointerDown pointDown)
						{
							if (InputState.Held(InputModule.MouseButton.Left) || InputState.Held(InputModule.MouseButton.Right))
								pointDown.OnPointerDown(pointerData);
						}
						if (handler is IPointerUp pointerUp)
						{
							if (InputState.Released(InputModule.MouseButton.Left) || InputState.Released(InputModule.MouseButton.Right))
								pointerUp.OnPointerUp(pointerData);
						}
						if(pointerHandler.BlockRaycast)
						{
							Pointer.IsOverObject = true;
						}
					}
					else
					{
						if(registreretPointerHandlers.Contains(pointerHandler))
						{
							registreretPointerHandlers.Remove(pointerHandler);
							if (handler is IPointerExit pointerExit)
							{
								pointerExit.OnPointerExit(pointerData);
							}
						}
					}
				}
			}
		}

		private bool IsOverPointerHandler(IPointerHandler handler)
		{
			Vector2 mouse = Vector2.Zero;
			if (handler.WorldSpace == WorldSpace.World)
				mouse = Camera.Main.ScreenToWorld(InputState.MousePosition);
			else if(handler.WorldSpace == WorldSpace.Screen)
				mouse = InputState.MousePosition;

			bool overlap = PhysicsModule.PhysicsIntersection.PointBox(mouse, handler.HandlerPosition, handler.HandlerSize, new Vector2(0.5f, 0.5f));
			return overlap;
		}

		protected override void Dispose(bool disposing)
		{
			if(!IsDisposed && disposing)
			{
				registreretPointerHandlers.Clear();
			}
			base.Dispose(disposing);
		}

		public override Predicate<IEventHandler> RemoveAllPredicate()
		{
			return item => item.Destroyed;
		}
	}
}