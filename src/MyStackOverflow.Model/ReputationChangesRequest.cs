using System.Collections.Generic;

namespace MyStackOverflow.Model
{
    public class ReputationChangesRequest
    {
        public int total { get; set; }
        public int page { get; set; }
        public int pagesize { get; set; }
        public List<RepChange> rep_changes { get; set; }
    }
}
