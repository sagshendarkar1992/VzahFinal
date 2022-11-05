using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPortal.User
{
   public class LayoutMenu
    {
        public LayoutMenu()
        { }
        //private int intMODULEID;

        public int IntMODULEID
        {
            get { return _IntMODULEID; }
            set { _IntMODULEID = value; }
        }
        private int _IntMODULEID;

        public string strModDesc
        {
            get { return _strModDesc; }
            set { _strModDesc = value; }
        }
        private string _strModDesc;


        public int IntPMenuID
        {
            get { return _IntPMenuID; }
            set { _IntPMenuID = value; }
        }
        private int _IntPMenuID;

        public string StrPMenuDesc
        {
            get { return _StrPMenuDesc; }
            set { _StrPMenuDesc = value; }
        }
        private string _StrPMenuDesc;

        public int IntMenuID
        {
            get { return _IntMenuID; }
            set { _IntMenuID = value; }
        }
        private int _IntMenuID;

        public string StrMenuName
        {
            get { return _StrMenuName; }
            set { _StrMenuName = value; }
        }
        private string _StrMenuName;

        public string StrMenuDesc
        {
            get { return _StrMenuDesc; }
            set { _StrMenuDesc = value; }
        }
        private string _StrMenuDesc;

        public string StrModDesc
        {
            get { return _StrModDesc; }
            set { _StrModDesc = value; }
        }
        private string _StrModDesc;

        public string ModStrIcon
        {
            get { return _ModStrIcon; }
            set { _ModStrIcon = value; }
        }
        private string _ModStrIcon;

        public string StrPageName
        {
            get { return _StrPageName; }
            set { _StrPageName = value; }
        }
        private string _StrPageName;

        public string StrCONTROLNAME
        {
            get { return _StrCONTROLNAME; }
            set { _StrCONTROLNAME = value; }
        }
        private string _StrCONTROLNAME;

        public int IntSYSDOCID
        {
            get { return _IntSYSDOCID; }
            set { _IntSYSDOCID = value; }
        }
        private int _IntSYSDOCID;

        public string StrIcon
        {
            get { return _StrIcon; }
            set { _StrIcon = value; }
        }
        private string _StrIcon;


        public string StrClassName
        {
            get { return _StrClassName; }
            set { _StrClassName = value; }
        }
        private string _StrClassName;

        public string StrMACESSKEY
        {
            get { return _StrMACESSKEY; }
            set { _StrMACESSKEY = value; }
        }
        private string _StrMACESSKEY;

        public int IntModMenuID
        {
            get { return _IntModMenuID; }
            set { _IntModMenuID = value; }
        }
        private int _IntModMenuID;

        public int ModMenuGrpID
        {
            get { return _ModMenuGrpID; }
            set { _ModMenuGrpID = value; }
        }
        private int _ModMenuGrpID;

        public string ModMenuGrpDesc
        {
            get { return _ModMenuGrpDesc; }
            set { _ModMenuGrpDesc = value; }
        }
        private string _ModMenuGrpDesc;


        public string GroupDescription
        {
            get { return _GroupDescription; }
            set { _GroupDescription = value; }
        }
        private string _GroupDescription;

        public List<LayoutAction> Actons = new List<LayoutAction>();

    }
    /// <summary>
    /// 
    /// </summary>
    public class LayoutAction
    {
        public string ACTDESC { get; set; }
        public string ACTNM { get; set; }
        public string CONTROLNAME { get; set; }
        public string SCRIPT { get; set; }

    }
}
