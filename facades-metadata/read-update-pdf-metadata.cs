using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfFileInfo and bind the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);

            // Read existing metadata
            Console.WriteLine($"Title: {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"PDF version: {pdfInfo.GetPdfVersion()}");

            // Modify metadata as needed
            pdfInfo.Title = "Updated Document Title";
            pdfInfo.Author = "John Doe";

            // Save the updated PDF to a new file
            pdfInfo.SaveNewInfo("updated.pdf");
        }

        Console.WriteLine("Metadata operations completed.");
    }
}