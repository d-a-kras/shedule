using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shedule.Models
{
    public class Shop
    {
        public List<WorkingDay> workingDays { get; set; }
        public List<DataForCalendary> holidayDays
        {
            get { return DFCs.Where(x => x.Tip == 8 || x.Tip == 9).ToList(); }
        }
        public List<NormaChas> NormaChasov = new List<NormaChas>();
        public List<employee> employes { get; set; }
        public List<employee> Semployes { get; set; }
        public List<TemplateWorkingDay> templates { get; set; }
        public List<daySale> daysSale { get; set; }
        public List<Factor> factors = new List<Factor>();
        public List<DataForCalendary> DFCs = new List<DataForCalendary>();
        public List<TSR> tsr = new List<TSR>();
        public List<TSR> tsrBG = new List<TSR>();
        public List<daySale> MouthPrognoz = new List<daySale>();
        public List<TemplateWorkingDay> MouthPrognozT = new List<TemplateWorkingDay>();
        public List<VarSmen> VarSmens = new List<VarSmen>();
        public List<MinRab> ListMinRab;
        public int RaznChas;
        public bool SortSotr = false;
        // public int countPrilavok = 0;
        public Prilavki prilavki;

        private int idShop;
        int idFM;

        private String address;
        public int getIdShop() { return idShop; }

        public int getIdShopFM() { return idFM; }
        public string getAddress() { return address; }



        public Shop(int i, string a, int i2)
        {

            this.idShop = i;
            this.address = a;
            this.idFM = i2;
            this.workingDays = new List<WorkingDay>();
            this.daysSale = new List<daySale>();
            this.employes = new List<employee>();
            this.factors = new List<Factor>();
            this.DFCs = new List<DataForCalendary>();
            this.templates = new List<TemplateWorkingDay>();

            Program.newShop();

        }

        public Shop(int i, string a)
        {

            idShop = i;
            address = a;
            this.workingDays = new List<WorkingDay>();
            this.daysSale = new List<daySale>();
            this.employes = new List<employee>();
            this.factors = new List<Factor>();
            this.DFCs = new List<DataForCalendary>();
            this.templates = new List<TemplateWorkingDay>();

            Program.newShop();

        }

        public mShop convertShoptoMShop() => new mShop(this.getIdShop(), this.getAddress());

        /* public void setMinRab(MinRab mr) { this.minrab = mr; }*/
        public void setMinRab(List<MinRab> listMinRab) { this.ListMinRab = listMinRab; }
        public void AddTemplate(TemplateWorkingDay t)
        {
            this.templates.Add(t);
        }

        public List<WorkingDay> getWorkingDays()
        {
            return this.workingDays;
        }

        public void setIdShop(int x)
        {
            this.idShop = x;
        }

        public void setIdShopFM(int x)
        {
            this.idFM = x;
        }

        public void setAdresShop(string x)
        {
            this.address = x;
        }

    }

}
