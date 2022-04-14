using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeoHypernation.BLL
{
    internal class WorkWithDoc
    {
        public bool ProcessWordDocument(object filename, object SaveAs)
        {
            Application wordApp = new Application();
            object missing = Missing.Value;

            Document WordDoc = null;

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

                HYP hyp = new HYP();
                wordApp = hyp.HYPExecute(wordApp);

                WordDoc.SaveAs2(ref SaveAs, ref missing, ref missing, ref missing,
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
        private void FindAndReplace(Application wordApp, object toFindText, object replaceWithText)
        {
            object matchCase = false;

            object matchwholeWord = true;

            object matchwildCards = true;

            object matchSoundLike = false;

            object nmatchAllforms = false;

            object forward = true;

            object format = false;

            object matchKashida = false;

            object matchDiactitics = false;

            object matchAlefHamza = false;

            object matchControl = false;

            object read_only = false;

            object visible = true;

            object replace = WdReplace.wdReplaceAll;

            object wrap = 1;

            wordApp.Selection.Find.Execute(ref toFindText, ref matchCase,
                                            ref matchwholeWord, ref matchwildCards, ref matchSoundLike,
                                            ref nmatchAllforms, ref forward,
                                            ref wrap, ref format, ref replaceWithText,
                                            ref replace, ref matchKashida,
                                            ref matchDiactitics, ref matchAlefHamza,
                                            ref matchControl);
        }
    }
}
