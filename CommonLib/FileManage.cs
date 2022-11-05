using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class FileManage
    {
        public Int64 FILEATTID
        {
            get { return _FILEATTID; }
            set { _FILEATTID = value; }
        }
        private Int64 _FILEATTID;

        public int MASTID
        {
            get { return _MASTID; }
            set { _MASTID = value; }
        }
        private int _MASTID;

        public int SYSDOCID
        {
            get { return _SYSDOCID; }
            set { _SYSDOCID = value; }
        }
        private int _SYSDOCID;

        public string SYSDOCDESC
        {
            get { return _SYSDOCDESC; }
            set { _SYSDOCDESC = value; }
        }
        private string _SYSDOCDESC;

        public string FNAME
        {
            get { return _FNAME; }
            set { _FNAME = value; }
        }
        private string _FNAME;

        public string FolderPath
        {
            get { return _FolderPath; }
            set { _FolderPath = value; }
        }
        private string _FolderPath;

        public decimal FileSizeKb
        {
            get { return _FileSizeKb; }
            set { _FileSizeKb = value; }
        }
        private decimal _FileSizeKb;

        public int ACTIONID
        {
            get { return _ACTIONID; }
            set { _ACTIONID = value; }
        }
        private int _ACTIONID;

        public string ACTNM
        {
            get { return _ACTNM; }
            set { _ACTNM = value; }
        }
        private string _ACTNM;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private int _UserID;

        public string USERNAME
        {
            get { return _USERNAME; }
            set { _USERNAME = value; }
        }
        private string _USERNAME;

        public DateTime EntryDate
        {
            get { return _EntryDate; }
            set { _EntryDate = value; }
        }
        private DateTime _EntryDate;

        public bool isactive
        {
            get { return _isactive; }
            set { _isactive = value; }
        }
        private bool _isactive;

        public int DocID
        {
            get { return _DocID; }
            set { _DocID = value; }
        }
        private int _DocID;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private string _remark;

        public string INVCNUMB
        {
            get { return _INVCNUMB; }
            set { _INVCNUMB = value; }
        }
        private string _INVCNUMB;

        public string INVCTYPE
        {
            get { return _INVCTYPE; }
            set { _INVCTYPE = value; }
        }
        private string _INVCTYPE;

        public string INVCDATE
        {
            get { return _INVCDATE; }
            set { _INVCDATE = value; }
        }
        private string _INVCDATE;

        public string Download
        {
            get { return _Download; }
            set { _Download = value; }
        }
        private string _Download;

        public string Remove
        {
            get { return _Remove; }
            set { _Remove = value; }
        }
        private string _Remove;

        public int TRANSID
        {
            get { return _TRANSID; }
            set { _TRANSID = value; }
        }
        private int _TRANSID;

    }
}
