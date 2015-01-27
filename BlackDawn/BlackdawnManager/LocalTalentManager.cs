using System;
using System.Collections;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Manager
{
	/// <summary>
	/// The LTM stores talents.
	/// </summary>
	public class LocalTalentManager
	{
		public LocalTalentManager()
		{
		}

		private Hashtable _talents = new Hashtable();

		protected Hashtable talents
		{
			get { return _talents; }
			set { _talents = value; }
		}

		/// <summary>
		/// Sets an talent to the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="talentID">The talent ID</param>
		/// <param name="value">The talent's value</param>
		public virtual void SetTalent(string scopeID, string talentID, Talent value)
		{
			string hash = String.Concat("t(", scopeID, "::", talentID, ")");
			talents[hash] = value;
		}

		/// <summary>
		/// Sets an talent to the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="talentID">The talent ID</param>
		/// <param name="value">The talent's value</param>
		public virtual void SetTalent(string fullyQualifiedName, Talent value)
		{
			talents[fullyQualifiedName] = value;
		}

		/// <summary>
		/// Gets an talent from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="talentID">The talent ID</param>
		/// <returns>talent</returns>
		public virtual Talent GetTalent(string scopeID, string talentID)
		{
			string hash = String.Concat("t(", scopeID, "::", talentID, ")");
			return GetTalent(hash);
		}

		/// <summary>
		/// Gets an talent from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="talentID">The talent ID</param>
		/// <returns>talent</returns>
		public virtual Talent GetTalent(string fullyQualifiedName)
		{
			return (Talent)talents[fullyQualifiedName];
		}
	}
}
