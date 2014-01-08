using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logan.Mall.Contract.DataContracts;

namespace Logan.Mall.Contract.ServiceContracts
{
    public interface IItemService
    {
        List<ItemInfo> GetAll();

        ItemInfo Get(string itemNO);
    }
}
