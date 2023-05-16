using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingElement.Business.Model {
    public enum Profession {
        [Description("")]
        NONE,
        [Description("建筑")]
        ARCHITECTURE,
        [Description("结构")]
        STRUCTURE,
        [Description("给排水")]
        PLUMBING,
        [Description("电气")]
        ELECTRICAL,
        [Description("暖通")]
        HVAC,
    }
}
