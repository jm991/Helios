using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helios.Model
{
    // This is the only way to get robust data binding + sample data support without IMultiValueConverter or using the SL hack version of it; ask SeanMcR if IMVCs work on Phone 8.1 since it converged with Windows 8.1
    public class ClipWrapper
    {
        public Clip ClipToWrap { get; set; }

        public ClipWrapper(Clip value)
        {
            ClipToWrap = value;
        }

        // Default constructor is required for usage as sample data 
        // in the WPF and Silverlight Designer.
        public ClipWrapper() {}
    }
}
