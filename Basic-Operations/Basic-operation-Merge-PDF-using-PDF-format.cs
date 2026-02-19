using System;
using System.IO;
using Aspose.Pdf;

class MergePdfExample
{
    static void Main(string[] args)
    {
        // Input PDF files to be merged
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output merged PDF file
        const string outputPdfPath = "merged.pdf";

        // Verify that input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the first document (this will be the base document)
            Document baseDoc = new Document(firstPdfPath);

            // Load the second document
            Document secondDoc = new Document(secondPdfPath);

            // Merge the second document into the base document by adding its pages.
            // The Document class does not expose AppendDocument; use Pages.Add instead.
            baseDoc.Pages.Add(secondDoc.Pages);

            // Save the merged document
            baseDoc.Save(outputPdfPath);

            Console.WriteLine($"PDF files merged successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during merging: {ex.Message}");
        }
    }
}
