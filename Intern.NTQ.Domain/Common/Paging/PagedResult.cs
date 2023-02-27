using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intern.NTQ.Library.Common
{
    public class PagedResult<T> : PageResultBase
    {
        public List<T> Items { get; set; }
    }
}
