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
        public String dateShowToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";
            if (date.Length > 4)
            {
                day = date.Substring(0,2);
                month = date.Substring(3, 2);
                year = date.Substring(6, 4);

                chk = year + "-" + month + "-" + day;
            }
            else
            {
                chk = date;
            }
            return chk;
        }
        public String dateYearToDB(String date)
        {
            String chk = "", year = "", month = "", day = "";
            if (date.Length > 4)
            {
                day = date.Substring(date.Length - 2);
                month = date.Substring(4, 2);
                year = date.Substring(0, 4);

                chk = year + "-" + month + "-" + day;
            }
            else
            {
                chk = date;
            }
            return chk;
        }
        public String dateTimeYearToDB(String datetime)
        {
            String chk = "", year = "", month = "", day = "", hh="", mm="";
            if (datetime.Length > 10)
            {
                hh = datetime.Substring(9, 2);
                mm = datetime.Substring(11, 2);

                day = datetime.Substring(6, 2);
                month = datetime.Substring(4, 2);
                year = datetime.Substring(0, 4);

                chk = year + "-" + month + "-" + day + " " + hh + ":" + mm;
            }
            else
            {
                chk = datetime;
            }
            

            return chk;
        }
        public String dateTimeYearToDB1(String datetime)
        {
            String chk = "", year = "", month = "", day = "", hh = "", mm = "", ss="";
            if (datetime.Length > 10)
            {
                hh = datetime.Substring(11, 2);
                mm = datetime.Substring(14, 2);
                ss = datetime.Substring(17, 2);

                day = datetime.Substring(8, 2);
                month = datetime.Substring(5, 2);
                year = datetime.Substring(0, 4);

                chk = year + "-" + month + "-" + day + " " + hh + ":" + mm + ":" + ss;
            }
            else
            {
                chk = datetime;
            }
            

            return chk;
        }
    }
}
