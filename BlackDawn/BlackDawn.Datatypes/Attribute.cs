using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MMT.BlackDawn.Datatypes
{
	/// <summary>
	/// Repräsentiert ein Attribut
	/// </summary>
	[XmlRoot(Namespace = "MMT://BlackDawn/Datatypes", ElementName = "Attribute", IsNullable = true)]
	public class Attribute : IBlackDawnType, IComparable<Attribute>, IFlaggable
	{
		public Attribute()
		{
		}

		public Attribute(float value)
		{
			_value = value;
		}

		private float _value = 0;

		/// <summary>
		/// Liefert oder setzt den Wert
		/// </summary>
		[XmlAttribute("value")]
		public float Value
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

		#region IComparable<Attribute> Members

		int IComparable<Attribute>.CompareTo(Attribute other)
		{
			return Value.CompareTo(other.Value);
		}

		#endregion

		#region Overrides

		public override string ToString()
		{
			return Value.ToString("0.0");
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is Attribute)
				return ((Attribute)obj).Value == Value;
			if (obj is float)
				return ((float)obj) == Value;
			throw new ArgumentException();
		}

		#endregion

		/// <summary>
		/// Operates on Attribute intepretations: Increments
		/// </summary>
		/// <param name="step">Step to increment</param>
		public void Increment(float step)
		{
			_value += step;
		}

		/// <summary>
		/// Operates on Attribute intepretations: Decrements
		/// </summary>
		/// <param name="step">Step to decrement</param>
		public void Decrement(float step)
		{
			_value -= step;
		}

		/// <summary>
		/// Returns whether the Flag is set or not
		/// </summary>
		public bool Flagged
		{
			get { return !NearZero(); }
		}

		private bool NearZero()
		{
			return (_value >= -0.0000001f) && (_value <= 0.0000001f);
		}

		/// <summary>
		/// Flags the value
		/// </summary>
		public void Flag()
		{
			_value = 1.0f;
		}

		/// <summary>
		/// Unflags the value
		/// </summary>
		public void Unflag()
		{
			_value = 0.0f;
		}

		/// <summary>
		/// Switches the flag
		/// </summary>
		public void SwitchFlag()
		{
			if (NearZero()) _value = 1.0f; else _value = 0.0f;
		}

		#region Cast-Operatoren

		public static implicit operator float(Attribute value)
		{
			return value.Value;
		}

		public static implicit operator Attribute(float value)
		{
			return new Attribute(value);
		}

		#endregion

		#region Vergleichsoperatoren

		public static bool operator ==(Attribute a, Attribute b)
		{
			return a.Value == b.Value;
		}

		public static bool operator !=(Attribute a, Attribute b)
		{
			return a.Value != b.Value;
		}

		public static bool operator <=(Attribute a, Attribute b)
		{
			return a.Value <= b.Value;
		}

		public static bool operator >=(Attribute a, Attribute b)
		{
			return a.Value >= b.Value;
		}

		public static bool operator <(Attribute a, Attribute b)
		{
			return a.Value < b.Value;
		}

		public static bool operator >(Attribute a, Attribute b)
		{
			return a.Value > b.Value;
		}

		#endregion

		#region Arithmetische Operatoren

		public static Attribute operator +(Attribute a, float value)
		{
			return new Attribute(a.Value + value);
		}

		public static float operator +(float value, Attribute a)
		{
			return a.Value + value;
		}

		public static Attribute operator -(Attribute a, float value)
		{
			return new Attribute(a.Value - value);
		}

		public static float operator -(float value, Attribute a)
		{
			return a.Value - value;
		}

		#endregion
	}
}
