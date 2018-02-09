using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollTyper.Mobile.Views;

namespace TrollTyper.Mobile.Models
{

    public class MasterDetailMenuItem
    {
        public string Title { get; set; }
        //public string IconName { get; protected set; }

        public Type TargetType { get; set; }
    }
}
