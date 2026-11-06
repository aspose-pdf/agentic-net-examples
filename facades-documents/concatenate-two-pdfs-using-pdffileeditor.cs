using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }

        // Lifecycle rule: wrap Document objects in using blocks for deterministic disposal
        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            // Append all pages from the source document to the target document
            target.Pages.Add(source.Pages);

            // Save the merged PDF; Document.Save(string) writes PDF regardless of extension
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}