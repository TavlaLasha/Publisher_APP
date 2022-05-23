using BLL.Contracts;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CleanManagement : ICleanManagement
    {
        public Application ExecuteAll(Application wordApp)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            wordApp = CleanSpaces(wordApp);
            wordApp = CleanNewLines(wordApp);
            wordApp = CleanTabs(wordApp);
            wordApp = CorrectPDashStarts(wordApp);

            return wordApp;
        }
        public Application CleanSpaces(Application wordApp)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            wordApp = FindAndReplace(wordApp, " [ ]@([! ])", @" \1");
            return wordApp;
        }

        public Application CleanNewLines(Application wordApp)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            wordApp = FindAndReplace(wordApp, "^11", "^13");
            return wordApp;
        }
        public Application CorrectPDashStarts(Application wordApp)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            wordApp = FindAndReplace(wordApp, "([-─])^13", @"—\1");
            return wordApp;
        }

        //For InDesign call CorrectPDashStarts and CleanTabs
        public Application CleanTabs(Application wordApp)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            wordApp = FindAndReplace(wordApp, "^9(^13)", @"\1");
            return wordApp;
        }

        private Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText)
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
