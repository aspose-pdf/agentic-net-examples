using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and create a PdfFileInfo instance for metadata operations
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Read existing metadata
            Console.WriteLine($"Title  : {pdfInfo.Title}");
            Console.WriteLine($"Author : {pdfInfo.Author}");
            Console.WriteLine($"Subject: {pdfInfo.Subject}");

            // Modify metadata as needed
            pdfInfo.Title   = "New Document Title";
            pdfInfo.Author  = "John Doe";
            pdfInfo.Subject = "Updated metadata example";

            // Save the PDF with the updated metadata
            pdfInfo.Save(outputPath);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
    }
}