using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output PDF file path
        const string outputPdfPath = "merged.pdf";

        // Verify that the source files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {secondPdfPath}");
            return;
        }

        // Load the first PDF document
        Document firstDoc = new Document(firstPdfPath);

        // Load the second PDF document
        Document secondDoc = new Document(secondPdfPath);

        // Append all pages from the second document to the first document
        firstDoc.Pages.Add(secondDoc.Pages);

        // Save the combined document (uses the provided document-save rule)
        firstDoc.Save(outputPdfPath);

        Console.WriteLine($"Documents merged successfully. Output saved to: {outputPdfPath}");
    }
}