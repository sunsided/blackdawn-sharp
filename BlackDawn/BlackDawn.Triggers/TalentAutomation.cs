using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using MMT.BlackDawn;
using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Triggers
{
	/// <summary>
	/// Class containting information about talent automation
	/// </summary>
	[XmlRoot("TalentAutomation")]
	public class TalentAutomation
	{
		/// <summary>
		/// Reload Rate per Second
		/// </summary>
		[XmlElement("RR")]
		public Datatypes.Attribute ReloadRate
		{
			get { return _ReloadRate; }
			set { _ReloadRate = value; }
		}

		private Datatypes.Attribute _ReloadRate;

		/// <summary>
		/// Reload Step
		/// </summary>
		[XmlElement("RS")]
		public Datatypes.Attribute ReloadStep
		{
			get { return _ReloadStep; }
			set { _ReloadStep = value; }
		}

		private Datatypes.Attribute _ReloadStep;
		
		/// <summary>
		/// Reload Enabled Condition
		/// </summary>
		[XmlElement("REC")]
		public Datatypes.IFlaggable ReloadEnabledCondition
		{
			get { return _ReloadEnabledCondition; }
			set { _ReloadEnabledCondition = value; }
		}

		private Datatypes.IFlaggable _ReloadEnabledCondition;

		/// <summary>
		/// Local Inversion Switch
		/// </summary>
		[XmlElement("LIS")]
		private Datatypes.IFlaggable _LocalInversion;

		public Datatypes.IFlaggable LocalInversion
		{
			get { return _LocalInversion; }
			set { _LocalInversion = value; }
		}

	}
}
