using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDFs and the output file
        string firstPdfPath = "first.pdf";
        string secondPdfPath = "second.pdf";
        string outputPdfPath = "merged.pdf";

        // Verify that both input files exist
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

        // Load the two PDF documents
        Document firstDoc = new Document(firstPdfPath);
        Document secondDoc = new Document(secondPdfPath);

        // Merge the pages of the second document into the first one
        firstDoc.Pages.Add(secondDoc.Pages);

        // Save the merged document (uses the provided document-save rule)
        firstDoc.Save(outputPdfPath);

        Console.WriteLine($"Merged PDF created successfully at: {outputPdfPath}");
    }
}