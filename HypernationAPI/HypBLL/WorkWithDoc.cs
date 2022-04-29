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
                object readOnly = true;

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
        //public static Application GetPage(object filename, int page = 1)
        //{
            //Application wordApp = new Application();
            //object missing = Missing.Value;

            //Document WordDoc;

            //if (File.Exists((string)filename))
            //{
            //    object readOnly = true;

            //    object isvisible = false;

            //    wordApp.Visible = false;
            //    WordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
            //                                        ref missing, ref missing, ref missing,
            //                                        ref missing, ref missing, ref missing,
            //                                        ref missing, ref missing, ref missing,
            //                                        ref missing, ref missing, ref missing, ref missing);
            //    WordDoc.Activate();

            //    var range = WordDoc.Range();
            //    range.Start = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, pageStart).Start;

            //    if (pageend < WordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, false))
            //    {
            //        range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, pageend + 1).End - 1;
            //    }

            //    range.Copy();

            //    WordDoc.Close();
            //    wordApp.Quit();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        //}
        public static bool ConvertToPDF(string input, string output, WdSaveFormat format)
        {
            try
            {
                // Create an instance of Word.exe
                _Application oWord = new Application
                {
                    // Make this instance of word invisible (Can still see it in the taskmgr).
                    Visible = false
                };

                // Interop requires objects.
                object oMissing = Missing.Value;
                object isVisible = true;
                object readOnly = true;     // Does not cause any word dialog to show up
                                            //object readOnly = false;  // Causes a word object dialog to show at the end of the conversion
                object oInput = input;
                object oOutput = output;
                object oFormat = format;

                // Load a document into our instance of word.exe
                _Document oDoc = oWord.Documents.Open(
                    ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                    );

                // Make this document the active document.
                oDoc.Activate();

                // Save this document using Word
                oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing
                    );

                // Always close Word.exe.
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
