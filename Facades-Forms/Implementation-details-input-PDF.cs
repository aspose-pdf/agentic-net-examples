using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileInfo facade and bind the PDF file
        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);

        // Read existing metadata
        Console.WriteLine($"Title: {pdfInfo.Title}");
        Console.WriteLine($"Author: {pdfInfo.Author}");
        Console.WriteLine($"Number of pages: {pdfInfo.NumberOfPages}");
        Console.WriteLine($"Is encrypted: {pdfInfo.IsEncrypted}");

        // Update metadata fields
        pdfInfo.Title = "Updated Title";
        pdfInfo.Author = "Updated Author";

        // Save the PDF with the new metadata (content remains unchanged)
        pdfInfo.SaveNewInfo(outputPath);

        // Release resources held by the facade
        pdfInfo.Close();

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}