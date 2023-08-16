
using Model;
using DAL;

namespace BL
{
    public class TabaccoBL
    {
        private TabaccoDAL idal = new TabaccoDAL();
        public Tabacco GetTabaccoById(int tabaccoId)
        {
            return idal.GetTabaccoById(tabaccoId);
        }
        
        public List<Tabacco> GetAll()
        {
            return idal.GetTabaccos(TabaccoFilter.GET_ALL, null);
        }

        
        public List<Tabacco> GetByName(string tabaccoName)
        {
            return idal.GetTabaccos(TabaccoFilter.FILTER_BY_Tabacco_NAME, new Tabacco{TabaccoName=tabaccoName});
        }
        
    }
}