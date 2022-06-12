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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class DocManagement : IDocManagement
    {
        public bool HyphenateDocument(object filename, object saveAs)
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
            catch(Exception ex)
            {
                wordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
                return false;
            }
        }

        public string HyphenateText(string text)
        {
            HYPManagement hyp = new HYPManagement(text);
            return hyp.HYPExecuteTxt();
        }

        public bool CleanDocument(object filename, object saveAs, DocCleanDTO docclDTo = null)
        {
            if (!File.Exists((string)filename))
                throw new HttpException("File does not exist in temporary storage");
            if (docclDTo == null)
                throw new HttpException("Doc cleaning model is null. Operation aborted as there will be nothing to do.");

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

                CleanManagement hyp = new CleanManagement(wordApp);
                wordApp = hyp.Execute(docclDTo);

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

        public string CleanText(string text, DocCleanDTO docclDTo = null)
        {
            if (docclDTo == null)
                throw new HttpException("Doc cleaning model is null. Operation aborted as there will be nothing to do.");

            CleanManagement hyp = new CleanManagement(text);
            return hyp.ExecuteTxt(docclDTo);
        }

        public object[] GetPages(object filename, int page = 1, bool clean = false)
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
                    if ((page + 2) >= PageCount)
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

                string tempDirectory = HttpContext.Current.Server.MapPath($"~/TempDocs/{Path.GetFileNameWithoutExtension(filename.ToString())}/HTML");
                if (Directory.Exists(tempDirectory) && clean)
                    Directory.Delete(tempDirectory, true);

                tempDirectory = Directory.CreateDirectory(tempDirectory).FullName;

                Dictionary<int, string> pages = new Dictionary<int, string>();
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
                        if (File.Exists(tempPath))
                        {
                            //using (StreamReader template = new StreamReader(tempPath))
                            //{
                            //    string text = ConvertHtmlToString(template, false);

                            //    template.Close();
                            //}
                            //var enc = Encoding.GetEncoding("ISO-8859-1");
                            //using (TextReader streamReader = new TextReader(tempPath, enc, true))
                            //{
                            //    string text = streamReader.ReadToEnd();
                            //    text = text.Replace("\n", " ");
                            //    text = text.Replace("\r", " ");
                            //    // Remove tab spaces
                            //    text = text.Replace("\t", " ");
                            //    pages.Add(i, text);
                            //}

                        }
                    }
                }
                WordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
                wordApp.Quit();


                //HTMLConverter s = new HTMLConverter();
                //string data = s.ConvertToHtml((string)tempPath);

                //if (File.Exists((string)tempPath)) {
                //    File.Delete((string)tempPath);
                //}

                return new object[2] { pages, PageCount.ToString() };
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

        public string HTMLToText(string HTMLCode)
        {
            // Remove new lines since they are not visible in HTML
            HTMLCode = HTMLCode.Replace(@"\n", " ");
            // Remove tab spaces
            HTMLCode = HTMLCode.Replace(@"\t", " ");
            // Remove multiple white spaces from HTML
            //HTMLCode = Regex.Replace(HTMLCode, "\\s+", " ");
            // Remove HEAD tag
            //HTMLCode = Regex.Replace(HTMLCode, "<head.*?</head>", ""
            //                    , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Remove any JavaScript
            //HTMLCode = Regex.Replace(HTMLCode, "<script.*?</script>", ""
            //  , RegexOptions.IgnoreCase | RegexOptions.Singleline);
            // Replace special characters like &, <, >, " etc.
            StringBuilder sbHTML = new StringBuilder(HTMLCode);
            // Note: There are many more special characters, these are just
            // most common. You can add new characters in this arrays if needed
    //        string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;",
    //"&gt;", "&reg;", "&copy;", "&bull;", "&trade;","&#39;"};
    //        string[] NewWords = { " ", "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢", "\'" };
    //        for (int i = 0; i < OldWords.Length; i++)
    //        {
    //            sbHTML.Replace(OldWords[i], NewWords[i]);
    //        }
            // Check if there are line breaks (<br>) or paragraph (<p>)
            //sbHTML.Replace("<br>", "\n<br>");
            //sbHTML.Replace("<br ", "\n<br ");
            //sbHTML.Replace("<p ", "\n<p ");
            // Finally, remove all HTML tags and return plain text
            return Regex.Replace(
              sbHTML.ToString(), "<[^>]*>", "");
        }

        //public string ConvertHtmlToString(TextReader streamToRead, bool isHtml)
        //{
        //    StringBuilder body = new StringBuilder();
        //    StringBuilder nextTag = new StringBuilder();
        //    bool inTag = false;
        //    char nextCharacter = char.MinValue;
        //    char tagStart = '$';

        //    while (streamToRead.Peek() >= 0)
        //    {
        //        nextCharacter = Convert.ToChar(streamToRead.Peek());
        //        if (nextCharacter.Equals(tagStart)) inTag = !inTag;

        //        if (inTag)
        //        {
        //            nextTag.Append(Convert.ToChar(streamToRead.Read()));
        //            if (nextTag.Length >= 50)
        //            {
        //                body.Append(nextTag.ToString());
        //                nextTag.Length = 0;
        //                inTag = false;
        //            }
        //        }
        //        else if (nextTag.Length > 0)
        //        {
        //            if (nextCharacter.Equals(tagStart)) nextTag.Append(Convert.ToChar(streamToRead.Read()));
        //            body.Append(nextTag.ToString());
        //            nextTag.Length = 0;
        //        }
        //        else
        //        {
        //            body.Append(Convert.ToChar(streamToRead.Read()));
        //        }
        //    }

        //    return body.ToString();
        //}

    }
}
