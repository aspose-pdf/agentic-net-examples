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

        // Use PdfFileInfo to read basic information (Title)
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            Console.WriteLine($"Title: {fileInfo.Title}");
        }

        // Retrieve page count via Document (PdfFileInfo does not expose PageCount in this version)
        int pageCount;
        using (Document tempDoc = new Document(inputPath))
        {
            pageCount = tempDoc.Pages.Count;
        }
        Console.WriteLine($"Page count: {pageCount}");

        // Load the PDF document, modify it, and save
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a blank page at the end
            doc.Pages.Add();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}