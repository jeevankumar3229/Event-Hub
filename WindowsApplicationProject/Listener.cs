using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationProject
{
    public class Listener
    {
        public string Name { get; set; }
        public string EventHubConnectionString { get; set; }
        
        public string ConsumerGroup { get; set; }
        public string StorageAccount { get; set; }
        public string Container { get; set; }
        public string Type { get; set; }
    }
}
