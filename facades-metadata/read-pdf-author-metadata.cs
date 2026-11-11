using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo implements IDisposable; use a using block for deterministic disposal
        using (PdfFileInfo info = new PdfFileInfo(pdfPath))
        {
            // Retrieve the Author metadata; fallback to empty string if null
            string author = info.Author ?? string.Empty;
            Console.WriteLine($"Author: {author}");
        }
    }
}