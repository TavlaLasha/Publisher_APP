using HypBLL.Interfaces;
using HypBLL.Logics;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HypBLL
{
    public class WorkWithDoc : IWorkWithDoc
    {
        public bool HypernateDocument(object filename, object saveAs)
        {
            if (!File.Exists((string)filename))
                throw new HttpException("File does not exist in temporary storage");

            Application wordApp = new Application();
            object missing = Missing.Value;

            Document WordDoc;

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

            WordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing);

            WordDoc.Close();
            wordApp.Quit();
            return true;
        }
        public string[] GetPages(object filename, int page = 1)
        {
            if (!File.Exists((string)filename))
                throw new HttpException("File does not exist in temporary storage");

            _Application wordApp = new Application();
            object missing = Missing.Value;

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
            if(page > PageCount || page < 1)
            {
                throw new InvalidOperationException("Page number exceeded document length");
            }

            string tempDirectory = Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/TempDocs/Temp/{Path.GetFileNameWithoutExtension(filename.ToString())}")).FullName;

            int start = ((page - 2) > 0) ? page - 2 : 1;
            int end;
            if (page <= 3)
            {
                end = (5 < PageCount) ? 5 : PageCount;
            }
            else
            {
                end = ((page + 2) < PageCount) ? page + 2 : PageCount;
            }

            Range range;
            for (int i = start; i <= end; i++)
            {
                range = WordDoc.Range();
                range.Start = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, i).Start;

                if (i < PageCount)
                {
                    range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, i + 1).End - 1;
                }
                else
                {
                    range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, i).End - 1;
                }
                string tempPath = Path.Combine(tempDirectory, $"{i}.html");
                if (!File.Exists(tempPath))
                {
                    range.ExportFragment(tempPath, WdSaveFormat.wdFormatFilteredHTML);
                }
            }
            WordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
            wordApp.Quit();


            //HTMLConverter s = new HTMLConverter();
            //string data = s.ConvertToHtml((string)tempPath);

            //if (File.Exists((string)tempPath)) {
            //    File.Delete((string)tempPath);
            //}

            return new string[2] { tempDirectory, PageCount.ToString() };
        }
        public bool ConvertToPDF(string input, string output, WdSaveFormat format)
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

        public bool ZipUpFiles(string dirPath, string outputPath)
        {
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            ZipFile.CreateFromDirectory(dirPath, outputPath);

            return true;
        }
    }
}
