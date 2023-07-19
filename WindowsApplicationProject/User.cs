using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsApplicationProject
{
    public class User
    {
        public string Name { get; set; }
        public string EventHubNamespace { get; set; }
        public string EventHubName { get; set; }
        public string ConsumerGroup { get; set; }
        public string StorageAccount { get; set; }
        public string Container { get; set; }
        public string Type { get; set; }
    }
}
