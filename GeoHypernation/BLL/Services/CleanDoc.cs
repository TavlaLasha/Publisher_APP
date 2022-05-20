using BLL.Contracts;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CleanDoc : ICleanDoc
    {
        private Application wordApp;
        public CleanDoc(Application wordApp)
        {
            this.wordApp = wordApp;
        }
        public Application Execute(bool cl_splace, bool cl_newLines, bool cor_PDashStarts, bool cl_tabs)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            if (cl_splace)
                wordApp = CleanSpaces(wordApp);

            if (cl_newLines)
                wordApp = CleanNewLines(wordApp);

            if (cor_PDashStarts)
                wordApp = CorrectPDashStarts(wordApp);

            if (cl_tabs)
                wordApp = CleanTabs(wordApp);

            return wordApp;
        }
        public Application CleanSpaces(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, " [ ]@([! ])", @" \1");
            return wordApp;
        }

        public Application CleanNewLines(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "^11", "^13");
            return wordApp;
        }
        public Application CorrectPDashStarts(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "([-─])^13", @"—\1");
            return wordApp;
        }

        //For InDesign
        public Application CleanTabs(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "^9(^13)", @"\1");
            return wordApp;
        }

        public Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText)
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

            try
            {
                wordApp.Selection.Find.Execute(ref toFindText, ref matchCase,
                                                ref matchwholeWord, ref matchwildCards, ref matchSoundLike,
                                                ref nmatchAllforms, ref forward,
                                                ref wrap, ref format, ref replaceWithText,
                                                ref replace, ref matchKashida,
                                                ref matchDiactitics, ref matchAlefHamza,
                                                ref matchControl);
                return wordApp;
            }
            catch (Exception e)
            {
                throw new Exception($"Err: {e.Message}");
            }
        }
    }
}
