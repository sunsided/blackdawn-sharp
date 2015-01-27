using System;
using System.Collections;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Manager
{
	/// <summary>
	/// A LTM for templated usage. If a value canot be taken from
	/// the local copy, it will be retrieved from the template.
	/// Values are stored in the local copy only.
	/// </summary>
	public class LocalTalentManagerTemplated : LocalTalentManager
	{
		/// <summary>
		/// Creates a LTM for templated usage. If a value canot be taken from
		/// the local copy, it will be retrieved from the template.
		/// Values are stored in the local copy only.
		/// </summary>
		/// <param name="template">The template to use</param>
		public LocalTalentManagerTemplated(LocalTalentManager template)
		{
			_template = template;
		}

		private LocalTalentManager _template = null;

		/// <summary>
		/// Gets an talent from the table
		/// </summary>
		/// <param name="scopeID">The scope ID</param>
		/// <param name="talentID">The talent ID</param>
		/// <returns>talent</returns>
		public override Talent GetTalent(string scopeID, string talentID)
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
		public override Talent GetTalent(string fullyQualifiedName)
		{
			object talent = talents[fullyQualifiedName];

			if( talent == null && _template != null )
				talent = _template.GetTalent(fullyQualifiedName);

			return (Talent)talent;
		}
	}
}
