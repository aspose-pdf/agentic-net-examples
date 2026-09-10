using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string resizedPath = "resized.pdf";
        const string outputPath = "rotated.pdf";
        const int pageNumber = 1; // page to rotate (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Resize page contents (keep original size: 100% width, 100% height)
        var fileEditor = new PdfFileEditor();
        fileEditor.ResizeContents(inputPath, resizedPath, new int[] { pageNumber }, 100, 100);

        // Rotate the specified page by 90 degrees
        var pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(resizedPath);
        pageEditor.PageRotations = new Dictionary<int, int>
        {
            { pageNumber, 90 } // rotate pageNumber by 90°
        };
        pageEditor.ApplyChanges();
        pageEditor.Save(outputPath);
        pageEditor.Close();

        Console.WriteLine($"Page {pageNumber} rotated and saved to '{outputPath}'.");
    }
}