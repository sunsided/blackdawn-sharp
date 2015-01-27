using System;
using System.Collections.Generic;
using System.Text;

namespace MMT.BlackDawn.Datatypes
{
	/// <summary>
	/// Kennzeichnet einen BlackDawn-Datentypen
	/// </summary>
	public interface IBlackDawnType
	{
		/// <summary>
		/// Liefert den Wert des Objektes als object
		/// </summary>
		/// <returns>object</returns>
		object GetValueAsObject();
	}
}
