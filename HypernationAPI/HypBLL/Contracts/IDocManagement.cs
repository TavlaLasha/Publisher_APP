﻿using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDocManagement
    {
        bool HypernateDocument(object filename, object saveAs);
        bool CleanDocument(object filename, object saveAs);
        string[] GetPages(object filename, int page = 1);
        bool ConvertToPDF(string input, string output, WdSaveFormat format);
        bool ZipUpFiles(string dirPath, string outputPath);
    }
}