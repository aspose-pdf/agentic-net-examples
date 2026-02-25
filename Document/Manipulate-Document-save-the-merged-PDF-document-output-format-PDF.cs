using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";

        // Output merged PDF file
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Wrap both Document objects in using blocks for deterministic disposal
        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            // Append all pages from the source document to the target document
            target.Pages.Add(source.Pages);

            // Save the merged document as a PDF (default format)
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}