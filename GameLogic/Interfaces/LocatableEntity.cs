using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Structs;

namespace Engine.Interfaces
{
    internal interface ILocatableEntity
    {
        public EntityLocation Location { get; set; }
    }


}
