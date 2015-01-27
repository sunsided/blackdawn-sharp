using System;
using System.Collections.Generic;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Triggers
{
	/// <summary>
	/// An automated talent
	/// </summary>
	public class SimpleAutomatedTalent : Talent, IPulseable
	{
		public SimpleAutomatedTalent(int in_intTalentID, TalentAutomation automation)
			: base(in_intTalentID)
		{
			_automation = automation;
		}

		public SimpleAutomatedTalent(SimpleAutomatedTalent talent)
			: base(talent)
		{
			_automation = talent.Automation;
		}

		private TalentAutomation _automation;

		/// <summary>
		/// Gets or sets the automation
		/// </summary>
		public TalentAutomation Automation
		{
			get { return _automation; }
			set { _automation = value; }
		}

		#region IPulseable Members

		/// <summary>
		/// Pulses the update
		/// </summary>
		/// <param name="msElapsed">The elapsed milliseconds since the last pulse</param>
		public void Pulse(long msElapsed)
		{
			if( (!_automation.LocalInversion.Flagged && _automation.ReloadEnabledCondition.Flagged ) ||
				 (_automation.LocalInversion.Flagged && !_automation.ReloadEnabledCondition.Flagged) )
			{
				// Bruchteile einer Sekunde
				float factor = Convert.ToSingle(msElapsed) * 0.0000001f; // 1 mHz
				// Rate * Bruchteile einer Sekunde
				float rate = _automation.ReloadRate.Value * factor;
				// Step * Rate
				float value = _automation.ReloadStep.Value * rate;
				// Wert addieren
				this.Value += value;

				// Plausibilitätscheck
				if (Value > ValueMax)
					Value = ValueMax;
				else if (Value < ValueMin)
					Value = ValueMin;
			}
		}

		#endregion
	}
}
