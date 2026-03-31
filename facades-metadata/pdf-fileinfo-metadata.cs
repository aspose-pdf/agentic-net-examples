using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and create a PdfFileInfo instance for metadata operations
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Read existing metadata
            Console.WriteLine($"Title: {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Subject: {pdfInfo.Subject}");
            Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"Is encrypted: {pdfInfo.IsEncrypted}");

            // Modify metadata fields
            pdfInfo.Title = "Updated Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Demo of PdfFileInfo metadata handling";

            // Save the updated metadata to a new PDF file
            pdfInfo.SaveNewInfo(outputPath);
        }

        Console.WriteLine($"Updated PDF saved as '{outputPath}'.");
    }
}