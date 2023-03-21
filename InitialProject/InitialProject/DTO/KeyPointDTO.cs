using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.DTO
{
    public class KeyPointDTO
    {
        public string Name { get; set; }

        public KeyPointDTO() { }    
        public KeyPointDTO(string name) 
        {  
            Name = name; 
        }
    }
}
