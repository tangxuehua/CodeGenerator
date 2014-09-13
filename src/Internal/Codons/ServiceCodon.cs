using System;
using System.Collections;
using System.Diagnostics;

using NetFocus.Components.AddIns.Codons;
using NetFocus.Components.AddIns.Attributes;
using NetFocus.Components.AddIns.Conditions;

namespace NetFocus.UtilityTool.CodeGenerator.AddIns.Codons
{
	[CodonTypeAttribute("Service")]
	public class ServiceCodon : AbstractCodon
	{

		public override object BuildItem(object owner, ArrayList subItems, ConditionCollection conditions)
		{
			return AddIn.CreateObject(Class);
		}
		
	}
}