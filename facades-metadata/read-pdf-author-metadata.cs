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

        // PdfFileInfo implements IDisposable, so use a using block for deterministic cleanup
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // Read the Author metadata property
            string author = fileInfo.Author;

            // Output the author to the console (empty string if not set)
            Console.WriteLine($"Author: {author}");
        }
    }
}