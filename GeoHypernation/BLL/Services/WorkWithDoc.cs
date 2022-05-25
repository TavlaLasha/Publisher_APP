using BLL.Contracts;
using BLL.Services;
using Microsoft.Office.Interop.Word;
using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Services
{
    public class WorkWithDoc : IWorkWithDoc
    {
        //private readonly SynchronizationContext syncContext;
        //private bool stopRequested;

        readonly string exeDir = Path.GetDirectoryName((new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath);
        public object DocPath;
        //private object SaveDocPath;
        private Application WordApp = null;
        private Document WordDoc = null;


        public WorkWithDoc(string docPath)
        {
            //syncContext = AsyncOperationManager.SynchronizationContext;
            TakeDoc(docPath);
            OpenDoc();
        }

        public void TakeDoc(string fileName)
        {
            if (File.Exists(fileName))
            {
                string FileName = Path.GetFileName(fileName);
                var contentType = Path.GetExtension(fileName);

                if (contentType == ".docx" || contentType == ".doc" || contentType == ".rtf")
                {
                    Directory.CreateDirectory(Path.Combine(exeDir, $"TempDocs"));
                    var FilePath = Path.Combine(exeDir, $"TempDocs\\{FileName}");
                    if (File.Exists(FilePath))
                    {
                        FileName = $"{Path.GetFileNameWithoutExtension(FileName)}{DateTime.Now.Ticks}{contentType}";
                        FilePath = Path.Combine(exeDir, $"TempDocs\\{FileName}");
                    }
                    File.Copy(fileName, FilePath);

                    DocPath = FilePath;
                }
                else
                    throw new Exception("Unsupported file type");
            }
            else
                throw new Exception("File not specified or does not exist");

        }
        public bool HypernateDocument()
        {
            if (WordDoc == null)
                throw new Exception("Document not open");

            HYP hyp = new HYP(WordApp);
            WordApp = hyp.HYPExecute();
            WordDoc.Save();


            return true;
        }
        public bool CleanDocument(bool cl_splace, bool cl_newLines, bool cor_PDashStarts, bool cl_tabs)
        {
            if (WordDoc == null)
                throw new Exception("Document not open");            

            CleanDoc hyp = new CleanDoc(WordApp);
            WordApp = hyp.Execute(cl_splace, cl_newLines, cor_PDashStarts, cl_tabs);
            WordDoc.Save();

            return true;
        }
        public DocDTO GetPages(int page = 1, bool clean = false)
        {
            if (WordDoc == null)
                throw new Exception("Document not open");


            int PageCount = WordDoc.ComputeStatistics(WdStatistic.wdStatisticPages, false);
            if (page < 1)
            {
                page = 1;
            }
            else if (page > PageCount)
            {
                page = PageCount;
            }
            int maxPages = 5;
            int start = ((page - 2) > 1) ? page - 2 : 1;
            int end;

            if (page <= 3)
            {
                end = (maxPages < PageCount) ? maxPages : PageCount;
            }
            else
            {
                if((page + 2) >= PageCount)
                {
                    start = PageCount - maxPages + 1;
                    if (start < 1)
                        start = 1;
                    end = PageCount;
                }
                else
                {
                    end = page + 2;
                }
            }
            string tempDirectory = Path.Combine(exeDir, $"TempDocs\\Temp\\{Path.GetFileNameWithoutExtension(DocPath.ToString())}");
            if (Directory.Exists(tempDirectory) && clean)
                Directory.Delete(tempDirectory, true);

            tempDirectory = Directory.CreateDirectory(tempDirectory).FullName;
            //TODO: will be out of bound at the end
            List<int> Pages = new List<int>();
            Range range;
            for (int i = start; i <= end+1; i++)
            {
                int index = i;
                if (index == end + 1)
                {
                    if (end == PageCount)
                        break;
                    index = PageCount;
                }

                string tempPath = Path.Combine(tempDirectory, $"{index}.html");
                if (!File.Exists(tempPath))
                {
                    range = WordDoc.Range();
                    range.Start = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, index).Start;

                    if (index < PageCount)
                    {
                        range.End = WordDoc.GoTo(WdGoToItem.wdGoToPage, WdGoToDirection.wdGoToAbsolute, index + 1).End - 1;
                    }
                    range.ExportFragment(tempPath, WdSaveFormat.wdFormatFilteredHTML);
                }
                if (File.Exists(tempPath))
                {
                    Pages.Add(index);
                }
            }

            //HTMLConverter s = new HTMLConverter();
            //string data = s.ConvertToHtml((string)tempPath);

            //if (File.Exists((string)tempPath))
            //{
            //    File.Delete((string)tempPath);
            //}

            return new DocDTO() { FileName = (string)DocPath, TempDirectory = tempDirectory, PageCount = PageCount, Pages = Pages };
        }
        public void OpenDoc()
        {
            if (!File.Exists((string)DocPath))
                throw new Exception("File not specified or does not exist in temporary storage");

            WordApp = new Application() { Visible = false };

            // Interop requires objects.
            object missing = Missing.Value;
            object readOnly = false;
            object isVisible = false;

            WordDoc = WordApp.Documents.Open(ref DocPath, ref missing, ref readOnly,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, ref missing,
                                                ref missing, ref missing, isVisible,
                                                ref missing, ref missing, ref missing, ref missing);
            WordDoc.Activate();
        }
        public void SaveDoc(string output, string format = ".docx")
        {
            if (!File.Exists((string)DocPath))
                throw new Exception("File not specified or does not exist in temporary storage");

            WdSaveFormat WDFormat;

            if (format.Equals(".doc"))
                WDFormat = WdSaveFormat.wdFormatDocument97;
            else if (format.Equals(".rtf"))
                WDFormat = WdSaveFormat.wdFormatRTF;
            else
                WDFormat = WdSaveFormat.wdFormatDocumentDefault;

            object missing = Missing.Value;
            object oFormat = WDFormat;
            object oOutput = output;

            WordDoc.SaveAs(ref oOutput, ref oFormat, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing);

            WordDoc.Close();
            WordApp.Quit(WdSaveOptions.wdSaveChanges);
            WordDoc = null;
            WordApp = null;
        }
        public void CloseDoc()
        {
            if (WordDoc != null && WordApp != null)
            {
                WordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
                WordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
            }
        }
    }
}
