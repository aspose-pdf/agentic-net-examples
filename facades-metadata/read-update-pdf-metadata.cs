using System;
using System.IO;
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

        // Create a PdfFileInfo instance and bind the PDF file
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);

            // Read existing metadata
            Console.WriteLine($"Title: {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"PDF version: {pdfInfo.GetPdfVersion()}");

            // Modify metadata (example)
            pdfInfo.Title = "Updated Title";
            pdfInfo.Author = "John Doe";

            // Save the PDF with updated metadata to a new file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}