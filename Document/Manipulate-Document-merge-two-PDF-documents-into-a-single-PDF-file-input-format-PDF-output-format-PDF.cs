using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "merged.pdf";

        // Verify that input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Wrap both source and target documents in using blocks for deterministic disposal
            using (Document target = new Document(firstPdfPath))
            using (Document source = new Document(secondPdfPath))
            {
                // Append all pages from the source document to the target document
                target.Pages.Add(source.Pages);

                // Save the merged document; Save() writes PDF regardless of extension
                target.Save(outputPdfPath);
            }

            Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}