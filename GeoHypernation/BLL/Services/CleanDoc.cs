using BLL.Contracts;
using Microsoft.Office.Interop.Word;
using Models.DataControlModels;
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
        public Application Execute(DocCleanDCM docClean)
        {
            if (wordApp == null)
                throw new Exception("No Document Given");

            if (docClean == null)
                throw new Exception("Invalid request: No instructions given.");

            if (docClean.cl_splace)
                wordApp = CleanSpaces(wordApp);

            if (docClean.cl_hyp)
                wordApp = CleanOldHyp(wordApp);

            if (docClean.cl_newLines)
                wordApp = CleanNewLines(wordApp);

            if (docClean.cl_par)
                wordApp = CleanExcParagraphs(wordApp);

            if (docClean.cor_PDashStarts)
                wordApp = CorrectPDashStarts(wordApp);

            if (docClean.cl_tabs)
                wordApp = CleanTabs(wordApp);

            return wordApp;
        }

        public Application CleanOldHyp(Application wordApp)
        {
            Console.WriteLine("CleanOldHyp");
            wordApp = FindAndReplace(wordApp, "^-", "");
            return wordApp;
        }

        public Application CleanExcParagraphs(Application wordApp)
        {
            Console.WriteLine("CleanSpaces");
            wordApp = FindAndReplace(wordApp, "^13{2}[^13]@([!^13])", @"^13\1");
            return wordApp;
        }

        public Application CleanSpaces(Application wordApp)
        {
            Console.WriteLine("CleanSpaces");
            wordApp = FindAndReplace(wordApp, " [ ]@([! ])", @" \1");
            return wordApp;
        }

        public Application CleanNewLines(Application wordApp)
        {
            Console.WriteLine("CleanNewLines");
            wordApp = FindAndReplace(wordApp, "^11", "^13");
            return wordApp;
        }
        public Application CorrectPDashStarts(Application wordApp)
        {
            Console.WriteLine("CorrectPDashStarts");
            wordApp = FindAndReplace(wordApp, "(^13)[-─]", @"\1— ");
            return wordApp;
        }

        //For InDesign
        public Application CleanTabs(Application wordApp)
        {
            Console.WriteLine("CleanTabs");
            wordApp = FindAndReplace(wordApp, "(^13)^9[^9]", @"\1");
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
