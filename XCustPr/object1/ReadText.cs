using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCustPr
{
    class ReadText
    {
        public ReadText()
        {

        }
        public List<String> ReadTextFile(String filename)
        {
            //String[] readText = new String[];
            List<String> result = new List<String>();
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead(filename))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    // Process line
                    if (line != string.Empty)
                    {
                        result.Add(line);
                    }
                }
            }
            return result;
        }
    }
}
