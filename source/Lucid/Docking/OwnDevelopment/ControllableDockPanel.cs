using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucid.Docking.OwnDevelopment
{
    public class ControllableDockPanel : DarkDockPanel
    {
        private static ControllableDockPanel _instance;

        private ControllableDockPanel()
        {

        }

        public static ControllableDockPanel GetInstance()
        {
            if (_instance == null)
                _instance = new ControllableDockPanel();

            return _instance;
        }
    }
}
