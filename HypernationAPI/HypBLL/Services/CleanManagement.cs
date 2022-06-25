using BLL.Contracts;
using Microsoft.Office.Interop.Word;
using Models.DataViewModels.DocManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class CleanManagement : ICleanManagement
    {
        private Application wordApp;
        private string Text;
        public CleanManagement(Application wordApp)
        {
            this.wordApp = wordApp;
        }
        public CleanManagement(string text)
        {
            Text = text;
        }
        public Application Execute(DocCleanDTO docClean)
        {
            if (wordApp == null)
                throw new HttpException("Invalid request: No Document Given");

            if (docClean == null)
                throw new HttpException("Invalid request: No instructions given.");

            if (docClean.CleanSpaces)
                wordApp = CleanSpaces(wordApp);

            if (docClean.CleanNewLines)
                wordApp = CleanNewLines(wordApp);

            if (docClean.CleanExcessParagraphs)
                wordApp = CleanExcParagraphs(wordApp);

            if (docClean.CorrectPDashStarts)
                wordApp = CorrectPDashStarts(wordApp);

            if (docClean.CleanTabs)
                wordApp = CleanTabs(wordApp);

            return wordApp;
        }

        public string ExecuteTxt(DocCleanDTO docClean)
        {
            if (Text == string.Empty || Text.Length < 3)
                throw new HttpException("Invalid request: No Text Given");

            if (docClean == null)
                throw new Exception("Invalid request: No instructions given.");

            //if (docClean.CleanSpaces)
            //    Text = CleanSpaces(Text);

            if (docClean.CleanExcessParagraphs)
                Text = CleanExcParagraphs(Text);

            if (docClean.CorrectPDashStarts)
                Text = CorrectPDashStarts(Text);

            if (docClean.CleanTabs)
                Text = CleanTabs(Text);

            return Text;
        }

        public Application CleanExcParagraphs(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "^13{2}[^13]@([!^13])", @"^13\1");
            return wordApp;
        }

        public Application CleanSpaces(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, " [ ]@([! ])", @" \1");
            wordApp = FindAndReplace(wordApp, @"(?) ([\.\,\;])", @"\1\2");
            return wordApp;
        }

        public Application CleanNewLines(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "^11", "^13");
            return wordApp;
        }
        public Application CorrectPDashStarts(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "(^13)[-─]", @"\1— ");
            return wordApp;
        }

        //For InDesign
        public Application CleanTabs(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "(^13)^9[^9]", @"\1");
            return wordApp;
        }

        //For Plain Text
        public string CleanExcParagraphs(string text)
        {
            var pattern = "\n\n+";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "\n\n");

            return text;
        }

        public string CleanSpaces(string text)
        {
            var pattern = @" +";
            var regex = new Regex(pattern);
            text = regex.Replace(text, @" ");

            return text;
        }

        public string CorrectPDashStarts(string text)
        {
            var pattern = "[-─]";
            var regex = new Regex(pattern);
            text = regex.Replace(text, @"—");

            return text;
        }

        public string CleanTabs(string text)
        {
            var pattern = "\t";
            var regex = new Regex(pattern);
            text = regex.Replace(text, " ");

            return text;
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
