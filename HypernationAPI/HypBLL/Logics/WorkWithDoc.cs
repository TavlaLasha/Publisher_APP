using HypBLL.Logics;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

                object isVisible = false;

                wordApp.Visible = false;
                WordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, isVisible,
                                                    ref missing, ref missing, ref missing, ref missing);
                WordDoc.Activate();

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
        //public static int GetPageCount(object filename)
        //{
        //    var application = new Application();

        //    object missing = Missing.Value;
        //    object readOnly = true;
        //    object isVisible = false;
        //    application.Visible = false;
        //    var document = application.Documents.Open(ref filename, ref missing, ref readOnly,
        //                                        ref missing, ref missing, ref missing,
        //                                        ref missing, ref missing, ref missing,
        //                                        ref missing, ref missing, isVisible,
        //                                        ref missing, ref missing, ref missing, ref missing);

        //    // Get the page count.
        //    var numberOfPages = document.ComputeStatistics(WdStatistic.wdStatisticPages, false);
        //    // Close word.
        //    //document.Close();
        //    application.Quit(WdSaveOptions.wdDoNotSaveChanges, ref missing, ref missing);

        //    return numberOfPages;
        //}
        public string[] GetPage(object filename, int page = 1)
        {
            if (File.Exists((string)filename))
            {
                _Application wordApp = new Application();
                object missing = Missing.Value;

                object tempPath = HttpContext.Current.Server.MapPath($"~/TempDocs/Temp/tmp_{DateTime.Now.Ticks}.docx");

                object readOnly = true;
                object isVisible = false;
                wordApp.Visible = false;

                _Document WordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref isVisible,
                                                    ref missing, ref missing, ref missing, ref missing);
                WordDoc.Activate();

                int PageCount = WordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, false);
                if(PageCount < page)
                {
                    throw new InvalidOperationException("Page number exceeded document length");
                }

                var range = WordDoc.Range();
                range.Start = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, page).Start;

                if (page < PageCount)
                {
                    range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, page + 1).End - 1;
                }
                //range.Copy();

                //_Application app = new Application();

                _Document doc2 = wordApp.Documents.Add();
                doc2.Activate();

                doc2.Range().FormattedText = range.FormattedText;

                WordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
                //wordApp.Quit();

                doc2.SaveAs(ref tempPath, ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing);
                doc2.Close(WdSaveOptions.wdDoNotSaveChanges);
                wordApp.Quit();


                HTMLConverter s = new HTMLConverter();
                string data = s.ConvertToHtml((string)tempPath);

                if (File.Exists((string)tempPath)) {
                    File.Delete((string)tempPath);
                }

                return new string[2] { data, PageCount.ToString() };
            }
            else
            {
                return new string[0];
            }
        }
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
