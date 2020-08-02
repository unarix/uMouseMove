using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uMouseMove.Entities.Helpers
{
    static class UIHelper
    {
        public static void ShowMessage(string message, MessageBoxIcon msgboxIcon)
        {
            MessageBox.Show(message, "Schedule", MessageBoxButtons.OK, msgboxIcon);
        }
    }
}
