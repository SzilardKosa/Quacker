using System;
using System.Collections.Generic;
using System.Text;

namespace Quacker.Dal.Dto
{
    public class UserHeader
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public bool HasImage { get; set; }
    }
}
