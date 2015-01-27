using System;
using System.Collections;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Manager
{

	/// <summary>
	/// The LVM stores attributes and values (states/flags)
	/// </summary>
	public class LocalValueManager
	{
		public LocalValueManager()
		{
		}

		private Hashtable _attributes = new Hashtable();

		protected Hashtable attributes
		{
			get { return _attributes; }
			set { _attributes = value; }
		}

		/// <summary>
		/// Sets an attribute to the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <param name="value">The attribute's value</param>
		public virtual void SetAttribute(string scopeID, string attributeID, Datatypes.Attribute value)
		{
			string hash = String.Concat("a(", scopeID, "::", attributeID, ")");
			attributes[hash] = value;
		}

		/// <summary>
		/// Sets an attribute to the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <param name="value">The attribute's value</param>
		public virtual void SetAttribute(string fullyQualifiedName, Datatypes.Attribute value)
		{
			attributes[fullyQualifiedName] = value;
		}

		/// <summary>
		/// Gets an attribute from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <returns>Attribute</returns>
		public virtual Datatypes.Attribute GetAttribute(string scopeID, string attributeID)
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
		public virtual Datatypes.Attribute GetAttribute(string fullyQualifiedName)
		{
			return (Datatypes.Attribute)attributes[fullyQualifiedName];
		}

		/// <summary>
		/// Sets a state/flag to the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="stateID">The state's ID</param>
		/// <param name="value">The state's value</param>
		public virtual void SetValue(string scopeID, string stateID, State value)
		{
			string hash = String.Concat("v(", scopeID, "::", stateID, ")");
			attributes[hash] = value;
		}

		/// <summary>
		/// Sets a state/flag to the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="stateID">The state's ID</param>
		/// <param name="value">The state's value</param>
		public virtual void SetValue(string fullyQualifiedName, State value)
		{
			attributes[fullyQualifiedName] = value;
		}

		/// <summary>
		/// Gets an attribute from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="attributeID">The attribute ID</param>
		/// <returns>Attribute</returns>
		public virtual State GetValue(string scopeID, string stateID)
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
		public virtual State GetValue(string fullyQualifiedName)
		{
			return (State)attributes[fullyQualifiedName];
		}
	}
}
