using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        // Verify the PDF file exists before attempting to read metadata.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfFileInfo implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo fileInfo = new PdfFileInfo(pdfPath))
        {
            // The Author property provides the author metadata of the PDF.
            string author = fileInfo.Author;

            // Output the author to the console. If the property is empty, an empty line is printed.
            Console.WriteLine($"Author: {author}");
        }
    }
}