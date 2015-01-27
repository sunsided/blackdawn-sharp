using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MMT.BlackDawn.Datatypes
{
	/// <summary>
	/// Repräsentiert einen Zustand bzw. ein Flag
	/// </summary>
	[XmlRoot(Namespace = "MMT://BlackDawn/Datatypes", ElementName = "State", IsNullable = true)]
	public class State : IBlackDawnType, IComparable<State>, IFlaggable, IState
	{
		public State()
		{
		}

		public State(int value)
		{
			_value = value;
		}

		private int _value = 0;

		/// <summary>
		/// Liefert oder setzt den Wert
		/// </summary>
		[XmlAttribute("value")]
		public int Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#region IBlackDawnType Members

		object IBlackDawnType.GetValueAsObject()
		{
			return Value;
		}

		#endregion

		#region IComparable<State> Members

		int IComparable<State>.CompareTo(State other)
		{
			return Value.CompareTo(other.Value);
		}

		#endregion

		#region Overrides

		public override string ToString()
		{
			return Value.ToString();
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is State)
				return ((State)obj).Value == Value;
			if (obj is int)
				return ((int)obj) == Value;
			throw new ArgumentException();
		}

		#endregion

		/// <summary>
		/// Gets the state
		/// </summary>
		/// <returns></returns>
		public int GetState()
		{
			return _value;
		}

		/// <summary>
		/// Sets the state
		/// </summary>
		public void SetState(int state)
		{
			_value = state;
		}

		/// <summary>
		/// Operates on State intepretations: Zeroes
		/// </summary>
		public void ZeroState()
		{
			_value = 0;
		}

		/// <summary>
		/// Operates on State intepretations: Icrements by 1
		/// </summary>
		public void Increment()
		{
			++_value;
		}

		/// <summary>
		/// Operates on State intepretations: Increments
		/// </summary>
		/// <param name="step">Step to increment</param>
		public void Increment(int step)
		{
			_value += step;
		}

		/// <summary>
		/// Operates on State intepretations: Decrements by 1
		/// </summary>
		public void Decrement()
		{
			--_value;
		}

		/// <summary>
		/// Operates on State intepretations: Decrements
		/// </summary>
		/// <param name="step">Step to decrement</param>
		public void Decrement(int step)
		{
			_value -= step;
		}

		/// <summary>
		/// Returns whether the Flag is set or not
		/// </summary>
		public bool Flagged
		{
			get { return _value != 0; }
		}

		/// <summary>
		/// Flags the value
		/// </summary>
		public void Flag()
		{
			_value = 1;
		}

		/// <summary>
		/// Unflags the value
		/// </summary>
		public void Unflag()
		{
			_value = 0;
		}

		#region Cast-Operatoren

		public static implicit operator int(State value)
		{
			return value.Value;
		}

		public static implicit operator State(int value)
		{
			return new State(value);
		}

		#endregion

		#region Vergleichsoperatoren

		public static bool operator ==(State a, State b)
		{
			return a.Value == b.Value;
		}

		public static bool operator !=(State a, State b)
		{
			return a.Value != b.Value;
		}

		public static bool operator <=(State a, State b)
		{
			return a.Value <= b.Value;
		}

		public static bool operator >=(State a, State b)
		{
			return a.Value >= b.Value;
		}

		public static bool operator <(State a, State b)
		{
			return a.Value < b.Value;
		}

		public static bool operator >(State a, State b)
		{
			return a.Value > b.Value;
		}

		#endregion

		#region Arithmetische Operatoren

		public static State operator +(State a, int value)
		{
			return new State(a.Value + value);
		}

		public static int operator +(int value, State a)
		{
			return a.Value + value;
		}

		public static State operator -(State a, int value)
		{
			return new State(a.Value - value);
		}

		public static int operator -(int value, State a)
		{
			return a.Value - value;
		}

		#endregion

		#region IFlaggable Members

		/// <summary>
		/// Switches the flag
		/// </summary>
		public void SwitchFlag()
		{
			if (_value == 0) _value = 1; else _value = 0;
		}

		#endregion
	}
}
