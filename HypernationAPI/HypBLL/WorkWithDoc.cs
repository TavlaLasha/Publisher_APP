using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HypBLL
{
    public class WorkWithDoc
    {
        public static bool ProcessWordDocument(object filename, object SaveAs)
        {
            Application wordApp = new Application();
            object missing = Missing.Value;

            Document WordDoc;

            if (File.Exists((string)filename))
            {
                object readOnly = false;

                object isvisible = false;

                wordApp.Visible = false;
                WordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing, ref missing);
                WordDoc.Activate();

                //Object xmlFormat = WdSaveFormat.wdFormatXML;
                //Object f = fileName;

                HYP hyp = new HYP();
                wordApp = hyp.HYPExecute(wordApp);

                WordDoc.SaveAs(ref SaveAs, ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing);

                WordDoc.Close();
                wordApp.Quit();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
