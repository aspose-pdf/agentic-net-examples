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

        // Read PDF file information using PdfFileInfo inside a using block
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            Console.WriteLine($"Pages: {fileInfo.NumberOfPages}");
            Console.WriteLine($"Author: {fileInfo.Author}");
            Console.WriteLine($"Title: {fileInfo.Title}");
        }

        // Load, modify, and save the PDF using Document inside a using block
        using (Document pdfDoc = new Document(inputPath))
        {
            pdfDoc.Pages.Add();                 // Example operation: add a blank page
            pdfDoc.Save(outputPath);            // Save the modified PDF
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}