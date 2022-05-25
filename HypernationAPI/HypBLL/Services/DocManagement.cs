using BLL.Contracts;
using BLL.Logics;
using BLL.Services;
using Microsoft.Office.Interop.Word;
using Models.DataViewModels.DocManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class DocManagement : IDocManagement
    {
        public bool HypernateDocument(object filename, object saveAs)
        {
            if (!File.Exists((string)filename))
                throw new HttpException("File does not exist in temporary storage");

            Application wordApp = new Application();
            object missing = Missing.Value;

            Document WordDoc;
            try
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

                HYPManagement hyp = new HYPManagement(wordApp);
                wordApp = hyp.HYPExecute();

                WordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing);

                WordDoc.Close();
                wordApp.Quit();
                return true;
            }
            catch
            {
                wordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
                return false;
            }
        }
        public bool CleanDocument(object filename, object saveAs, DocCleanDTO docclDTo = null)
        {
            if (!File.Exists((string)filename))
                throw new HttpException("File does not exist in temporary storage");
            if (docclDTo != null && (!docclDTo.CleanNewLines && !docclDTo.CleanSpaces && !docclDTo.CleanTabs && !docclDTo.CorrectPDashStarts))
            {
                throw new HttpException("Nothing to do");
            }

            Application wordApp = new Application();
            object missing = Missing.Value;

            Document WordDoc;

            object readOnly = true;

            object isVisible = false;

            try
            {
                wordApp.Visible = false;
                WordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, isVisible,
                                                    ref missing, ref missing, ref missing, ref missing);
                WordDoc.Activate();

                CleanManagement hyp = new CleanManagement();
                if (docclDTo == null || (docclDTo.CleanNewLines && docclDTo.CleanSpaces && docclDTo.CleanTabs && docclDTo.CorrectPDashStarts))
                {
                    wordApp = hyp.ExecuteAll(wordApp);
                }
                else if (docclDTo != null)
                {
                    if (docclDTo.CleanSpaces)
                        wordApp = hyp.CleanSpaces(wordApp);

                    if (docclDTo.CleanNewLines)
                        wordApp = hyp.CleanNewLines(wordApp);

                    if (docclDTo.CleanTabs)
                        wordApp = hyp.CleanTabs(wordApp);

                    if (docclDTo.CorrectPDashStarts)
                        wordApp = hyp.CorrectPDashStarts(wordApp);
                }

                WordDoc.SaveAs(ref saveAs, ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing);

                WordDoc.Close();
                wordApp.Quit();
                return true;
            }
            catch
            {
                wordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
                return false;
            }
        }
        public string[] GetPages(object filename, int page = 1)
        {
            if (!File.Exists((string)filename))
                throw new HttpException("File does not exist in temporary storage");

            _Application wordApp = new Application();
            object missing = Missing.Value;

            object readOnly = true;
            object isVisible = false;
            try
            {
                wordApp.Visible = false;

                _Document WordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref missing,
                                                    ref missing, ref missing, ref isVisible,
                                                    ref missing, ref missing, ref missing, ref missing);
                WordDoc.Activate();

                int PageCount = WordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, false);
                if (page > PageCount || page < 1)
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
                    string tempPath = Path.Combine(tempDirectory, $"{i}.html");
                    if (!File.Exists(tempPath))
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
            catch
            {
                wordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
                return new string[0];
            }
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

            try
            {
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
                oWord.Quit(WdSaveOptions.wdDoNotSaveChanges);
                return false;
            }
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
