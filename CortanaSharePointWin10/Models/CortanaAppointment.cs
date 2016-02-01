using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Core;

namespace CortanaSharePointWin10.Models
{
    public class CortanaAppointment : IAppointment
    {
        public string Id { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }

        public string Subject { get; set; }
        public string Details { get; set; }

        public string Location { get; set; }
        public override string ToString()
        {
            return this.Subject;
        }
    }
}
