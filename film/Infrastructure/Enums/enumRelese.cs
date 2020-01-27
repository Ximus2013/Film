using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace film.Infrastructure.Enums
{
    public enum EnumForRelese
    {
        [Description("No Released")]
        NoReleased = 0,
        [Description("Released")]
        Released = 1,
        [Description("Finished")]
        Finished = 2,
    }
}