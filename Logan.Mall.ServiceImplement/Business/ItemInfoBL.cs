using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logan.Mall.Contract.ServiceContracts;
using Logan.Mall.Contract.DataContracts;
using Logan.Mall.ServiceImplement.DataAccess;

namespace Logan.Mall.ServiceImplement.Business
{
    public class ItemInfoBL : IItemService
    {
        public List<ItemInfo> GetAll()
        {
            try
            {
                return ItemInfoDA.GetAll();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ItemInfo Get(string itemNO)
        {
            try
            {
                return ItemInfoDA.Get(itemNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
