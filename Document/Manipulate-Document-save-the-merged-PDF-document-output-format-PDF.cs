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
        // Output merged PDF path
        const string outputPdfPath = "merged.pdf";

        // Verify that input files exist
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

        // Load the first PDF document (create and load)
        Document firstDoc = new Document(firstPdfPath);
        // Load the second PDF document
        Document secondDoc = new Document(secondPdfPath);

        // Merge the second document into the first one
        firstDoc.Pages.Add(secondDoc.Pages);

        // Save the merged document (simple save without options)
        firstDoc.Save(outputPdfPath);

        Console.WriteLine($"Merged PDF saved to: {outputPdfPath}");
    }
}