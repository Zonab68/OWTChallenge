using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWTChallenge.Core.Aggregates
{
    public class ContactSkillsWithLevel
    {
        public int ContactId { get; set; }  
        public int ContactName { get; set; }
        public int LevelId { get; set; }    
        public string LevelName { get; set; }   
        public int SkillId { get; set; }    
        public string SkillName { get; set; }   
    }
}
