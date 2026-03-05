using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Optional source PDF whose pages will be appended
        const string sourcePdf = "source.pdf";
        // Output PDF file path
        const string outputPdf = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the target document inside a using block for deterministic disposal
        using (Document targetDoc = new Document(inputPdf))
        {
            // Add a blank page at the end of the document
            // This creates an empty page using the most common page size in the document
            targetDoc.Pages.Add();

            // If a source PDF is provided, append all its pages to the target document
            if (File.Exists(sourcePdf))
            {
                using (Document sourceDoc = new Document(sourcePdf))
                {
                    // Append all pages from sourceDoc to targetDoc
                    targetDoc.Pages.Add(sourceDoc.Pages);
                }
            }

            // Save the modified document as PDF
            targetDoc.Save(outputPdf);
        }

        Console.WriteLine($"Pages added and document saved to '{outputPdf}'.");
    }
}