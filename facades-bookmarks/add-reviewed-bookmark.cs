using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (PdfBookmarkEditor editor = new PdfBookmarkEditor())
        {
            editor.BindPdf(inputPath);
            Document doc = editor.Document;
            int lastPageNumber = doc.Pages.Count;
            editor.CreateBookmarkOfPage("Reviewed", lastPageNumber);
            editor.Save(outputPath);
        }

        Console.WriteLine("Bookmark added and saved to '" + outputPath + "'.");
    }
}