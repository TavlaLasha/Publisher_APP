﻿using BLL.Contracts;
using BLL.Services;
using Microsoft.Office.Interop.Word;
using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class WorkWithDoc : IWorkWithDoc
    {
        readonly string exeDir = Path.GetDirectoryName((new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath);
        private object DocPath;
        private object SaveDocPath;
        private Application wordApp = new Application();
        public WorkWithDoc(string docPath)
        {
            DocPath = docPath;
        }
        public bool HypernateDocument()
        {
            if (!File.Exists((string)DocPath))
                throw new Exception("File not specified or does not exist in temporary storage");

            object missing = Missing.Value;

            Document WordDoc;

            object readOnly = true;

            object isVisible = false;

            wordApp.Visible = false;
            WordDoc = wordApp.Documents.Open(ref DocPath, ref missing, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, isVisible,
                                                ref missing, ref missing, ref missing, ref missing);
            WordDoc.Activate();

            HYP hyp = new HYP(wordApp);
            wordApp = hyp.HYPExecute();

            WordDoc.SaveAs(ref SaveDocPath, ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing);

            WordDoc.Close();
            return true;
        }
        public bool CleanDocument(bool cl_splace, bool cl_newLines, bool cor_PDashStarts, bool cl_tabs)
        {
            if (!File.Exists((string)DocPath))
                throw new Exception("File not specified or does not exist in temporary storage");

            object missing = Missing.Value;

            Document WordDoc;

            object readOnly = true;

            object isVisible = false;

            wordApp.Visible = false;
            WordDoc = wordApp.Documents.Open(ref DocPath, ref missing, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, isVisible,
                                                ref missing, ref missing, ref missing, ref missing);
            WordDoc.Activate();

            CleanDoc hyp = new CleanDoc(wordApp);
            wordApp = hyp.Execute(cl_splace, cl_newLines, cor_PDashStarts, cl_tabs);

            WordDoc.SaveAs(ref SaveDocPath, ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing);

            WordDoc.Close();
            return true;
        }
        public DocDTO GetPages(int page = 1)
        {
            if (!File.Exists((string)DocPath))
                throw new Exception("File not specified or does not exist in temporary storage");

            object missing = Missing.Value;

            object readOnly = true;
            object isVisible = false;
            wordApp.Visible = false;

            _Document WordDoc = wordApp.Documents.Open(ref DocPath, ref missing, ref readOnly,
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

            string tempDirectory = Directory.CreateDirectory(Path.Combine(exeDir, $"TempDocs\\Temp\\{Path.GetFileNameWithoutExtension(DocPath.ToString())}")).FullName;

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
            List<int> Pages = new List<int>();
            Range range;
            for (int i = start; i <= end+1; i++)
            {
                range = WordDoc.Range();
                int index = i;
                if (index == end + 1)
                    index = PageCount;

                range.Start = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, index).Start;
                
                if (i < PageCount)
                {
                    range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, index + 1).End - 1;
                }
                else
                {
                    range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, index).End - 1;
                }
                string tempPath = Path.Combine(tempDirectory, $"{index}.html");
                if (!File.Exists(tempPath))
                {
                    range.ExportFragment(tempPath, WdSaveFormat.wdFormatFilteredHTML);
                }
                if (File.Exists(tempPath))
                {
                    Pages.Add(i);
                }
            }

            WordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);


            //HTMLConverter s = new HTMLConverter();
            //string data = s.ConvertToHtml((string)tempPath);

            //if (File.Exists((string)tempPath))
            //{
            //    File.Delete((string)tempPath);
            //}

            return new DocDTO() { FileName = (string)DocPath, TempDirectory = tempDirectory, PageCount = PageCount, Pages = Pages };
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
    }
}