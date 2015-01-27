using System;
using System.Collections.Generic;
using System.Text;

using MMT.BlackDawn.Datatypes;
#pragma warning disable 0660

namespace MMT.BlackDawn.Triggers
{
	public class TriggeredFlag<t> : IBlackDawnType, IFlaggable, ITrigger, IPulseable where t : IFlaggable, IBlackDawnType
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

		public TriggeredFlag()
		{
		}

		/// <summary>
		/// Constructs the triggered flag
		/// </summary>
		/// <param name="ttl">The time to live</param>
		public TriggeredFlag(t flag, long ttl)
		{
			SurroundedValue = flag;
			_startttl = _ttl = ttl;
		}

		/// <summary>
		/// Constructs the triggered flag
		/// </summary>
		/// <param name="ttl">The time to live</param>
		/// <param name="action">The action to perform at the end of TTL</param>
		public TriggeredFlag(t flag, long ttl, TriggerFlagAction action)
			: this( flag, ttl)
		{
			_action = action;
		}

		/// <summary>
		/// Constructs the triggered flag
		/// </summary>
		/// <param name="ttl">The time to live</param>
		/// <param name="action">The action to perform at the end of TTL</param>
		/// <param name="affection">The time effect affection state</param>
		public TriggeredFlag(t flag, long ttl, TriggerFlagAction action, AffectionState affection)
			: this(flag, ttl, action)
		{
			_affection = affection;
		}

		/// <summary>
		/// Constructs the triggered flag
		/// </summary>
		/// <param name="ttl">The time to live</param>
		/// <param name="action">The action to perform at the end of TTL</param>
		/// <param name="affection">The time effect affection state</param>
		public TriggeredFlag(t flag, long ttl, TriggerFlagAction action, AffectionState affection, ReactionMode reaction)
			: this(flag, ttl, action, affection)
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

		private TriggerFlagAction _action = TriggerFlagAction.Unflag;

		/// <summary>
		/// Gets or Sets the end action
		/// </summary>
		public TriggerFlagAction Action
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
		public event EndOfLifeFlagMonitor EndOfLifeArrived = null;

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

				// Flag
				switch (_action)
				{
					case TriggerFlagAction.Flag:
						{
							SurroundedValue.Flag();
							break;
						}
					default:
					case TriggerFlagAction.Unflag:
						{
							SurroundedValue.Unflag();
							break;
						}
					case TriggerFlagAction.Switch:
						{
							SurroundedValue.SwitchFlag();
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

		#region IFlaggable Members

		public void Flag()
		{
			_value.Flag();
		}

		public bool Flagged
		{
			get { return _value.Flagged; }
		}

		public void Unflag()
		{
			_value.Unflag();
		}

		public void SwitchFlag()
		{
			_value.SwitchFlag();
		}

		#endregion

		#region Vergleichsoperatoren

		public static bool operator ==(TriggeredFlag<t> a, t b)
		{
			return a.SurroundedValue.GetValueAsObject() == b.GetValueAsObject();
		}

		public static bool operator !=(TriggeredFlag<t> a, t b)
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
