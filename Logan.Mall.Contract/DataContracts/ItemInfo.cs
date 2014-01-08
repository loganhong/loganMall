
using Logan.Mall.BaseLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logan.Mall.Contract.DataContracts
{
    public class ItemInfo
    {
        [DataMapping]
        public int ID { get; set; }

        [DataMapping]
        public string ItemNO { get; set; }

        [DataMapping]
        public decimal SalePrice { get; set; }

        [DataMapping]
        public string ItemName { get; set; }

        [DataMapping]
        public string CreateUser { get; set; }

        [DataMapping]
        public DateTime CreateDate { get; set; }

        public string EditUser { get; set; }
        [DataMapping]
        public DateTime? EditDate { get; set; }
    }
}
