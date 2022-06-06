using BLL.Interfaces;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class HYPManagement : IHYPManagement
    {
        private Application wordApp;
        private string Text;
        public HYPManagement(Application wordApp)
        {
            this.wordApp = wordApp;
        }
        public HYPManagement(string text)
        {
            Text = text;
        }
        public Application HYPExecute()
        {
            if (wordApp == null)
                throw new HttpException("No Document Given");

            wordApp = HYPWovels(wordApp);
            wordApp = HYPConsonants(wordApp);
            wordApp = CleanFirst(wordApp);
            wordApp = CleanLast(wordApp);
            wordApp = CleanConstr(wordApp);
            wordApp = CleanLastConpunct(wordApp);
            wordApp = CleanHarmonics(wordApp);

            return wordApp;
        }
        public string HYPExecuteTxt()
        {
            if (Text == string.Empty || Text.Length < 3)
                throw new HttpException("No Text Given");

            Text = HYPWovels(Text);
            Text = HYPConsonants(Text);
            Text = CleanFirst(Text);
            Text = CleanLast(Text);
            Text = CleanConstr(Text);
            Text = CleanLastConpunct(Text);
            Text = CleanHarmonics(Text);

            return Text;
        }
        public Application HYPConsonants(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "([ბ-დვ-თკ-ნპ-ტფ-ჰ])([ბ-დვ-თკ-ნპ-ტფ-ჰ][აეიოუ])", "" + $@"\1{((char)31).ToString()}\2");
            return wordApp;
        }

        public Application HYPWovels(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, "([აეიოუ])", $@"\1{((char)31).ToString()}");
            return wordApp;
        }

        public Application CleanFirst(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, @"([\ \«\-\—\─\(\„\”][ა-ჰ])" + ((char)31).ToString(), @"\1");
            return wordApp;
        }

        public Application CleanLast(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, @"([ა-ჰ])" + ((char)31).ToString() + @"([\ \.\,\!\?\)\-\;\:\»\“^13])", @"\1\2");
            wordApp = FindAndReplace(wordApp, ((char)31).ToString() + @"([ა-ჰ][\ \.\,\!\?\)\-\;\:\»\“^13])", @"\1");
            return wordApp;
        }
        public Application CleanHarmonics(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, $@"([ბდზთპტფღყჩცწჭხჯ]){((char)31).ToString()}([ბდზთპტფღყჩცწჭხჯ])", @"\1\2");
            return wordApp;
        }
        public Application CleanConstr(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, ((char)31).ToString() + $@"([ბ-დვ-თკ-ნპ-ტფ-ჰ]{((char)31).ToString()})", @"\1");
            return wordApp;
        }

        public Application CleanLastConpunct(Application wordApp)
        {
            wordApp = FindAndReplace(wordApp, $@"{((char)31).ToString()}(ი[სხნ][\ \.\,\!\?\)\-\;\:\“^13])", @"\1");
            return wordApp;
        }

        //For Palin Text
        public string HYPConsonants(string text)
        {
            var pattern = @"([ბ-დვ-თკ-ნპ-ტფ-ჰ])([ბ-დვ-თკ-ნპ-ტფ-ჰ][აეიოუ])";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "$1\xad$2");

            return text;
        }

        public string HYPWovels(string text)
        {
            var pattern = "([აეიოუ])";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "$1\xad");

            return text;
        }

        public string CleanFirst(string text)
        {
            var pattern = $@"([\ \«\-\—\─\(\„\”][ა-ჰ]){"\xad"}";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "$1");

            return text;
        }

        public string CleanLast(string text)
        {
            //var pattern = $@"^(([ა-ჰ]){"\xad"}([\ \.\,\!\?\)\-\;\:\»\“^13])|{"\xad"}([ა-ჰ])([\ \.\,\!\?\)\-\;\:\»\“^13]))$";
            //var regex = new Regex(pattern);
            //text = regex.Replace(text, "$1$2");
            //return text;

            var pattern = $@"([ა-ჰ]){"\xad"}([\ \.\,\!\?\)\-\;\:\»\“^13])";
            var pattern2 = $@"{"\xad"}([ა-ჰ][\ \.\,\!\?\)\-\;\:\»\“^13])";
            var regex = new Regex(pattern);
            var regex2 = new Regex(pattern2);

            text = regex.Replace(text, "$1$2");
            text = regex2.Replace(text, "$1");
            return text;
        }
        public string CleanHarmonics(string text)
        {
            var pattern = $@"([ბდზთპტფღყჩცწჭხჯ]){"\xad"}([ბდზთპტფღყჩცწჭხჯ])";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "$1$2");

            return text;
        }
        public string CleanConstr(string text)
        {
            var pattern = $@"{"\xad"}([ბ-დვ-თკ-ნპ-ტფ-ჰ]{"\xad"})";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "$1");

            return text;
        }

        public string CleanLastConpunct(string text)
        {
            var pattern = $@"{"\xad"}(ი[სხნ][\ \.\,\!\?\)\-\;\:\“^13])";
            var regex = new Regex(pattern);
            text = regex.Replace(text, "$1");

            return text;
        }

        public Application FindAndReplace(Application wordApp, object toFindText, object replaceWithText)
        {
            object matchCase = false;

            object matchwholeWord = false;

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
