using System;
using System.Collections.Generic;
using System.Text;

using MMT.BlackDawn.Datatypes;

namespace MMT.BlackDawn.Triggers
{
	/// <summary>
	/// An automated talent
	/// </summary>
	public class AutomatedTalent : Talent, IPulseable
	{
		public AutomatedTalent(int in_intTalentID, TalentAutomation automation)
			: base(in_intTalentID)
		{
			_automation.Add(automation);
		}

		public AutomatedTalent(int in_intTalentID, TalentAutomation[] automations)
			: base(in_intTalentID)
		{
			_automation.AddRange(automations);
		}

		public AutomatedTalent(AutomatedTalent talent)
			: base(talent)
		{
			_automation = new List<TalentAutomation>(talent.Automations);
		}

		private List<TalentAutomation> _automation = new List<TalentAutomation>();

		/// <summary>
		/// Gets or sets the automation
		/// </summary>
		public TalentAutomation[] Automations
		{
			get { return _automation.ToArray(); }
			set { _automation = new List<TalentAutomation>(value); }
		}

		/// <summary>
		/// Adds an automation to the list
		/// </summary>
		/// <param name="automation">The automation to add</param>
		public void AddAutomation(TalentAutomation automation)
		{
			if (!_automation.Contains(automation)) _automation.Add(automation);
		}

		/// <summary>
		/// Removes an automation from the list
		/// </summary>
		/// <param name="automation">The automation to remove</param>
		public void RemoveAutomation(TalentAutomation automation)
		{
			_automation.Remove(automation);
		}

		#region IPulseable Members

		/// <summary>
		/// Pulses the update
		/// </summary>
		/// <param name="msElapsed">The elapsed milliseconds since the last pulse</param>
		public void Pulse(long msElapsed)
		{
			TalentAutomation[] automation = _automation.ToArray();
			lock (automation)
			{
				foreach (TalentAutomation item in automation)
				{
					if ((!item.LocalInversion.Flagged && item.ReloadEnabledCondition.Flagged) ||
						 (item.LocalInversion.Flagged && !item.ReloadEnabledCondition.Flagged))
					{
						// Bruchteile einer Sekunde
						float factor = Convert.ToSingle(msElapsed) * 0.0000001f; // 1 mHz
						// Rate * Bruchteile einer Sekunde
						float rate = item.ReloadRate.Value * factor;
						// Step * Rate
						float value = item.ReloadStep.Value * rate;
						// Wert addieren
						this.Value += value;
					}
				}

			}

			// Plausibilitätscheck
			if (Value > ValueMax)
				Value = ValueMax;
			else if (Value < ValueMin)
				Value = ValueMin;
		}

		#endregion
	}
}
