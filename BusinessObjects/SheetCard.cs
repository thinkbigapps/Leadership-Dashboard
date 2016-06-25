using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class SheetCard
    {
        public int sheetID { get; set; }
        public int cardSlot { get; set; }
        public string cardName { get; set; }
        public string requestedDate { get; set; }
        public string awardDate { get; set; }
        public string awardedBy { get; set; }
        public string awardMethod { get; set; }
        public string awardNote { get; set; }
    }
}
