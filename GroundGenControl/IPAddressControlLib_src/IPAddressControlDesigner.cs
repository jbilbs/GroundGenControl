using System;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace IPAddressControlLib
{
	public class IPAddressControlDesigner : ControlDesigner
	{
      public override SelectionRules SelectionRules
      {
         get
         {
            IPAddressControl ctrl = (IPAddressControl)Control;

            if ( ctrl.AutoSize )
            {
               return SelectionRules.LeftSizeable | SelectionRules.RightSizeable | SelectionRules.Moveable | SelectionRules.Visible;
            }
            else
            {
               return SelectionRules.AllSizeable | SelectionRules.Moveable | SelectionRules.Visible;
            }
         }
      }
 	}
}
