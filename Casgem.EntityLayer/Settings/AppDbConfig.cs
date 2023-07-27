using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.EntityLayer.Settings
{
    public class AppDbConfig
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string StartupLogsCollectionName { get; set; } = null!;
        public string OtherCollectionName { get; set; } = null!;
    }
}
