using System;
using System.Collections.Generic;
using System.Text;

using MMT.BlackDawn.Datatypes;
#pragma warning disable 0660

namespace MMT.BlackDawn.Triggers
{
	public class TriggeredState<t> : IBlackDawnType, IState, ITrigger, IPulseable where t : IState, IBlackDawnType
	{
		private object _tag = null;

		/// <summary>
		/// Gets or sets a user-defined tag
		/// </summary>
		public object Tag
		{
			get { return _tag; }
			set { _tag = value; }
		}

		/// <summary>
		/// One second in milliseconds
		/// </summary>
		public static long DurationOneSecondMS = 10000000;

		/// <summary>
		/// Ten seconds in milliseconds
		/// </summary>
		public static long DurationTenSecondsMS = 100000000;

		/// <summary>
		/// One minute in milliseconds
		/// </summary>
		public static long DurationOneMinuteMS = 600000000;

		/// <summary>
		/// One hour in milliseconds
		/// </summary>
		public static long DurationOneHourMS = 36000000000;

		public TriggeredState()
		{
		}

		/// <summary>
		/// Constructs the triggered state
		/// </summary>
		/// <param name="ttl">The time to live</param>
		public TriggeredState(t state, long ttl)
		{
			SurroundedValue = state;
			_startttl = _ttl = ttl;
		}

		/// <summary>
		/// Constructs the triggered state
		/// </summary>
		/// <param name="ttl">The time to live</param>
		/// <param name="action">The action to perform at the end of TTL</param>
		public TriggeredState(t state, long ttl, TriggerStateAction action)
			: this( state, ttl)
		{
			_action = action;
		}

		/// <summary>
		/// Constructs the triggered state
		/// </summary>
		/// <param name="ttl">The time to live</param>
		/// <param name="action">The action to perform at the end of TTL</param>
		/// <param name="affection">The time effect affection state</param>
		public TriggeredState(t state, long ttl, TriggerStateAction action, AffectionState affection)
			: this(state, ttl, action)
		{
			_affection = affection;
		}

		/// <summary>
		/// Constructs the triggered state
		/// </summary>
		/// <param name="ttl">The time to live</param>
		/// <param name="action">The action to perform at the end of TTL</param>
		/// <param name="affection">The time effect affection state</param>
		public TriggeredState(t state, long ttl, TriggerStateAction action, AffectionState affection, ReactionMode reaction)
			: this(state, ttl, action, affection)
		{
			_reaction = reaction;
		}

		/// <summary>
		/// Registers the trigger at the global timer manager (GTM)
		/// </summary>
		protected void Register()
		{
			Triggers.LocalTimerManager.RegisterTrigger(this, _affection);
		}

		/// <summary>
		/// Registers the trigger at the global timer manager (GTM)
		/// </summary>
		protected void Unregister()
		{
			Triggers.LocalTimerManager.UnregisterTrigger(this, _affection);
		}

		private ReactionMode _reaction = ReactionMode.Terminate;

		/// <summary>
		/// Gets or sets the reaction mode
		/// </summary>
		public ReactionMode Reaction
		{
			get { return _reaction; }
			set { _reaction = value; }
		}

		private TriggerStateAction _action = TriggerStateAction.Increment;

		/// <summary>
		/// Gets or Sets the end action
		/// </summary>
		public TriggerStateAction Action
		{
			get { return _action; }
			set { _action = value; }
		}
		private AffectionState _affection = AffectionState.Affected;

		/// <summary>
		/// Gets or Sets the affection state
		/// </summary>
		public AffectionState Affection
		{
			get { return _affection; }
			set 
			{
				if (_enabled) throw new InvalidOperationException("Trigger still running. Affection state not changed.");
				if (_affection != value)
				{
					Unregister();
					_affection = value;
					Register();
				}
			}
		}

		private bool _enabled = false;

		/// <summary>
		/// Gets whether the trigger is running
		/// </summary>
		public bool Running { get { return _enabled; } }

		/// <summary>
		/// Enables and starts the trigger
		/// </summary>
		public void Start()
		{
			_ttl = _startttl;
			Register();
			_enabled = true;
			//Pulse(0);
		}

		/// <summary>
		/// Sets the trigger to sleep
		/// </summary>
		public void Suspend()
		{
			_enabled = false;
		}

		/// <summary>
		/// Enforces the end action
		/// </summary>
		public void FastForward()
		{
			_ttl = 0;
			_enabled = true;
			Pulse(0);
		}

		private long _ttl = 1000; // 10 seconds
		private long _startttl = 1000; // 10 seconds

		/// <summary>
		/// Gets or sets the time to live for the object
		/// </summary>
		public long TimeToLive
		{
			get { return _ttl; }
			set { _ttl = value; _startttl = value; }
		}

		/// <summary>
		/// This event is fired if the trigger has reached End of Life
		/// </summary>
		public event EndOfLifeStateMonitor EndOfLifeArrived = null;

		/// <summary>
		/// Pulses the trigger
		/// </summary>
		/// <param name="millisecElapsed">The elapsed time since the last pulse</param>
		public void Pulse(long millisecElapsed)
		{
			if (!_enabled) return;
			_ttl -= millisecElapsed;
			
			// Kondition für EndAction
			if (_ttl <= 0)
			{
				_enabled = false;

				// state
				switch (_action)
				{
					case TriggerStateAction.Increment:
						{
							SurroundedValue.Increment();
							break;
						}
					default:
					case TriggerStateAction.Decrement:
						{
							SurroundedValue.Decrement();
							break;
						}
					case TriggerStateAction.Zero:
						{
							SurroundedValue.ZeroState();
							break;
						}
				}

				// Aufruf der Hilfsfunktion
				if (EndOfLifeArrived != null) EndOfLifeArrived.BeginInvoke(SurroundedValue, this, null, null);

				// Reaction
				switch (_reaction)
				{
					default:
					case ReactionMode.Terminate:
						{
							Unregister();
							break;
						}
					case ReactionMode.Restart:
						{
							Start();
							break;
						}
				}
			}
		}

		private t _value;

		public t SurroundedValue
		{
			get { return _value; }
			set { _value = value; }
		}

		#region IBlackDawnType Members

		object IBlackDawnType.GetValueAsObject()
		{
			return _value.GetValueAsObject();
		}

		#endregion

		#region Istategable Members

		public void Increment()
		{
			_value.Increment();
		}

		public void Decrement()
		{
			_value.Decrement();
		}

		public void ZeroState()
		{
			_value.ZeroState();
		}

		public int GetState()
		{
			return _value.GetState();
		}

		public void SetState(int value)
		{
			_value.SetState(value);
		}

		#endregion

		#region Vergleichsoperatoren

		public static bool operator ==(TriggeredState<t> a, t b)
		{
			return a.SurroundedValue.GetValueAsObject() == b.GetValueAsObject();
		}

		public static bool operator !=(TriggeredState<t> a, t b)
		{
			return a.SurroundedValue.GetValueAsObject() != b.GetValueAsObject();
		}

		#endregion

		//public override bool Equals(object obj)
		//{
		//    return ((object)this).Equals(obj);
		//}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}
	}
}
