using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MMT.BlackDawn.Datatypes
{
	/// <summary>
	/// Spezifisches Talent eines Charakters
	/// </summary>
	[XmlRoot(Namespace = "MMT://BlackDawn/Datatypes", ElementName = "Talent", IsNullable = true)]
	public class Talent : IBlackDawnType, IComparable<Talent>
	{
		#region Exceptions
		public class TalentNotMatchingException : Exception
		{
			public TalentNotMatchingException()
				: base()
			{
			}

			public TalentNotMatchingException(string message)
				: base(message)
			{
			}
		}
		#endregion

		/// <summary>
		/// Maximale Stärke/Stufe/Dauer des Talents (-1 wenn nicht zutreffend)
		/// </summary>
		/// <remarks>Dies ist beispielsweise nötig, um bestimmte Werte zu flaggen: Mana, etwa, oder die Fähigkeit zu tauchen (Talent "Luft anhalten")</remarks>
		private float m_valueMax = -1;

		/// <summary>
		/// Minimale Stärke/Stufe/Dauer des Talents
		/// </summary>
		/// <remarks>Dies ist beispielsweise nötig, um bestimmte Werte zu flaggen: Mana, etwa, oder die Fähigkeit zu tauchen (Talent "Luft anhalten")</remarks>
		private float m_valueMin = 0;

		/// <summary>
		/// Liefert oder setzt den Maximalwert des Talentes
		/// Maximale Stärke/Stufe/Dauer des Talents (-1 wenn nicht zutreffend)
		/// </summary>
		[XmlElement(ElementName = "max", DataType = "float")]
		public float ValueMax
		{
			get { return this.m_valueMax; }
			set { this.m_valueMax = value; }
		}

		/// <summary>
		/// Liefert oder setzt den Minimalwert des Talentes
		/// Minimale Stärke/Stufe/Dauer des Talents
		/// </summary>
		[XmlElement(ElementName = "min", DataType = "float")]
		public float ValueMin
		{
			get { return this.m_valueMin; }
			set { this.m_valueMin = value; }
		}

		/// <summary>
		/// Derzeitige Stärke/Stufe des Talents
		/// </summary>
		private float m_value = 0;

		/// <summary>
		/// Liefert oder setzt den tatsächlichen Wert des Talentes
		/// </summary>
		[XmlElement(ElementName = "value", DataType = "float")]
		public float Value
		{
			get { return this.m_value; }
			set { this.m_value = value; }
		}

		/// <summary>
		/// Die systemweite ID des Talentes
		/// </summary>
		private int m_lngTalentID;

		/// <summary>
		/// Liefert die ID des Talentes
		/// </summary>
		[XmlAttribute("id", DataType = "int")]
		public int TalentID
		{
			get { return this.m_lngTalentID; }
		}


		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="in_floatTalentID">Die systemweite ID des Talentes</param>
		public Talent(int in_intTalentID)
		{
			this.m_lngTalentID = in_intTalentID;
			this.m_value = 0.0f;
			this.m_valueMax = -1;
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="in_Talent">Das zu kopierende Talent</param>
		public Talent(Talent in_Talent)
		{
			this.m_lngTalentID = in_Talent.m_lngTalentID;
			this.m_value = in_Talent.m_value;
			this.m_valueMax = in_Talent.m_valueMax;
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="in_floatValue">Startwert des Talents</param>
		/// <param name="in_floatTalentID">Die systemweite ID des Talentes</param>
		public Talent(int in_intTalentID, float in_floatValue)
		{
			this.m_lngTalentID = in_intTalentID;
			this.m_value = in_floatValue;
			this.m_valueMax = -1;
		}

		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="in_floatValue">Startwert des Talents</param>
		/// <param name="in_floatValueMax">Maximaler Wert des Talents</param>
		/// <param name="in_floatTalentID">Die systemweite ID des Talentes</param>
		public Talent(int in_intTalentID, float in_floatValue, float in_floatValueMax)
		{
			this.m_lngTalentID = in_intTalentID;
			this.m_value = in_floatValue;
			this.m_valueMax = in_floatValueMax;
		}

		/// <summary>
		/// Cast-Operator Talent zu float
		/// </summary>
		/// <param name="in_Talent">Talent</param>
		/// <returns>Talent-ID</returns>
		public static implicit operator int(Talent in_Talent)
		{
			return in_Talent.TalentID;
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public static bool operator ==(Talent in_Talent1, Talent in_Talent2)
		{
			if (in_Talent1.TalentID == in_Talent2.TalentID)
			{
				if (in_Talent1.Value == in_Talent2.Value) return true;
			}
			return false;
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public static bool operator !=(Talent in_Talent1, Talent in_Talent2)
		{
			if (in_Talent1.TalentID == in_Talent2.TalentID)
			{
				if (in_Talent1.Value == in_Talent2.Value) return false;
			}
			return true;
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public override bool Equals(object obj)
		{
			if (obj.GetType() != this.GetType()) return false;
			if (((Talent)obj).TalentID == this.TalentID)
			{
				if (((Talent)obj).Value == this.Value) return true;
			}
			return false;
		}

		/// <summary>
		/// Liefert den Hashcode des Talentes
		/// </summary>
		/// <returns>bool</returns>
		public override int GetHashCode()
		{
			return this.TalentID.GetHashCode();
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public static bool operator <(Talent in_Talent1, Talent in_Talent2)
		{
			if (in_Talent1.TalentID != in_Talent2.TalentID)
			{
				throw new System.ArgumentException("Talent 2 hat eine andere ID als Talent 1", "in_Talent2");
			}
			return (in_Talent1.Value < in_Talent2.Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public static bool operator >(Talent in_Talent1, Talent in_Talent2)
		{
			if (in_Talent1.TalentID != in_Talent2.TalentID)
			{
				throw new TalentNotMatchingException("Talent 2 hat eine andere ID als Talent 1");
			}
			return (in_Talent1.Value > in_Talent2.Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public static bool operator <=(Talent in_Talent1, Talent in_Talent2)
		{
			if (in_Talent1.TalentID != in_Talent2.TalentID)
			{
				throw new TalentNotMatchingException("Talent 2 hat eine andere ID als Talent 1");
			}
			return (in_Talent1.Value <= in_Talent2.Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Talent
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Talent2">Talent 2</param>
		/// <returns>bool</returns>
		public static bool operator >=(Talent in_Talent1, Talent in_Talent2)
		{
			if (in_Talent1.TalentID != in_Talent2.TalentID)
			{
				throw new TalentNotMatchingException("Talent 2 hat eine andere ID als Talent 1");
			}
			return (in_Talent1.Value >= in_Talent2.Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static bool operator <(Talent in_Talent1, float in_Value)
		{
			return (in_Talent1.Value < in_Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static bool operator >(Talent in_Talent1, float in_Value)
		{
			return (in_Talent1.Value > in_Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static bool operator <=(Talent in_Talent1, float in_Value)
		{
			return (in_Talent1.Value <= in_Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static bool operator >=(Talent in_Talent1, float in_Value)
		{
			return (in_Talent1.Value >= in_Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static bool operator ==(Talent in_Talent1, float in_Value)
		{
			return (in_Talent1.Value == in_Value);
		}

		/// <summary>
		/// Vergleichs-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static bool operator !=(Talent in_Talent1, float in_Value)
		{
			return (in_Talent1.Value != in_Value);
		}

		/// <summary>
		/// Additions-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static Talent operator +(Talent in_Talent, float in_Value)
		{
			Talent newTalent = new Talent(in_Talent);
			newTalent.Value += in_Value;
			return newTalent;
		}

		/// <summary>
		/// Subtraktions-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static Talent operator -(Talent in_Talent, float in_Value)
		{
			Talent newTalent = new Talent(in_Talent);
			newTalent.Value -= in_Value;
			return newTalent;
		}

		/// <summary>
		/// Multiplikations-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static Talent operator *(Talent in_Talent, float in_Value)
		{
			Talent newTalent = new Talent(in_Talent);
			newTalent.Value *= in_Value;
			return newTalent;
		}

		/// <summary>
		/// Divisions-Operator Talent mit Integer
		/// </summary>
		/// <param name="in_Talent1">Talent 1</param>
		/// <param name="in_Value">Wert</param>
		/// <returns>bool</returns>
		public static Talent operator /(Talent in_Talent, float in_Value)
		{
			Talent newTalent = new Talent(in_Talent);
			newTalent.Value /= in_Value;
			return newTalent;
		}

		#region IBlackDawnType Members

		object IBlackDawnType.GetValueAsObject()
		{
			return this.m_value;
		}

		#endregion

		#region IComparable<Talent> Members

		int IComparable<Talent>.CompareTo(Talent other)
		{
			int value = TalentID.CompareTo(other.TalentID);
			if (value == 0) value = Value.CompareTo(other.Value);
			if (value == 0) value = ValueMax.CompareTo(other.ValueMax);
			return value;
		}

		#endregion
	}
}
