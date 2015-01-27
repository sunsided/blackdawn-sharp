using System;
using System.Collections;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Manager
{
	/// <summary>
	/// A LVM for templated usage. If a value canot be taken from
	/// the local copy, it will be retrieved from the template.
	/// Values are stored in the local copy only.
	/// </summary>
	public class LocalValueManagerTemplated : LocalValueManager
	{

		/// <summary>
		/// Creates a LVM for templated usage. If a value canot be taken from
		/// the local copy, it will be retrieved from the template.
		/// Values are stored in the local copy only.
		/// </summary>
		/// <param name="template">The template to use</param>
		public LocalValueManagerTemplated(LocalValueManager template)
		{
			_template = template;
		}

		private LocalValueManager _template = null;

		/// <summary>
		/// Gets an attribute from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <returns>Attribute</returns>
		public override Datatypes.Attribute GetAttribute(string scopeID, string attributeID)
		{
			string hash = String.Concat("a(", scopeID, "::", attributeID, ")");
			return GetAttribute(hash);
		}

		/// <summary>
		/// Gets an attribute from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <returns>Attribute</returns>
		public override Datatypes.Attribute GetAttribute(string fullyQualifiedName)
		{
			object attribute = attributes[fullyQualifiedName];

			// Try to use template, if it exists
			if (attribute == null && _template != null)
				attribute = _template.GetAttribute(fullyQualifiedName);

			return (Datatypes.Attribute)attribute;
		}

		/// <summary>
		/// Gets an attribute from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <returns>Attribute</returns>
		public override State GetValue(string scopeID, string stateID)
		{
			string hash = String.Concat("v(", scopeID, "::", stateID, ")");
			return GetValue(hash);
		}

		/// <summary>
		/// Gets an attribute from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <returns>Attribute</returns>
		public override State GetValue(string fullyQualifiedName)
		{
			object value = attributes[fullyQualifiedName];
			// Try to use template, if it exists
			if (value == null && _template != null)
				value = _template.GetValue(fullyQualifiedName);

			return (State)value;
		}
	}
}
