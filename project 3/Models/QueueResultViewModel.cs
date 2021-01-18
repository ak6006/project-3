using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_3.Models
{
    public class QueueResultViewModel
    {
        public string Serial { get; set; }
        public string Result { get; set; }
    }

    public class VQueueResultViewModel
    {
        public int Counter { get; set; }
        public string Serial { get; set; }
        public string Result { get; set; }
    }
}