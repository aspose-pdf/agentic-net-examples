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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve basic PDF metadata using PdfFileInfo.
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            Console.WriteLine($"Pages: {info.NumberOfPages}");
            Console.WriteLine($"Author: {info.Author}");
            Console.WriteLine($"Title: {info.Title}");
        }

        // Perform low‑level PDF manipulation with Document.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Add a blank page at the end of the document.
            pdfDoc.Pages.Add();

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
