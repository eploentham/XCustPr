using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    public class Persistent
    {
        public String table = "";
        public String pkField = "";
        public String sited = "";
        Random r = new Random();
        public String getGenID()
        {
            return r.Next().ToString();
        }
        public String dateYearToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";

            day = date.Substring(date.Length - 2);
            month = date.Substring(4, 2);
            year = date.Substring(0, 4);

            chk = year+"-" + month+"-" + day;

            return chk;
        }
        public String dateTimeYearToDB(String datetime)
        {
            String chk = "", year = "", month = "", day = "", hh="", mm="";

            hh = datetime.Substring(9, 2);
            mm = datetime.Substring(11, 2);

            day = datetime.Substring(6,2);
            month = datetime.Substring(4, 2);
            year = datetime.Substring(0, 4);

            chk = year + "-" + month + "-" + day+" "+hh+":"+mm;

            return chk;
        }
    }
}
