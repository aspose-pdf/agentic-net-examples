using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string sourcePdf = "source.pdf";   // Main document
        const string extraPdf  = "extra.pdf";    // Document whose pages will be added
        const string outputPdf = "output.pdf";   // Resulting file

        // Verify that the input files exist
        if (!File.Exists(sourcePdf) || !File.Exists(extraPdf))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        // Load the main document inside a using block for deterministic disposal
        using (Document doc = new Document(sourcePdf))
        {
            // Add a blank page at the end of the document
            doc.Pages.Add();

            // Load the extra document (also in a using block)
            using (Document extra = new Document(extraPdf))
            {
                // Append all pages from the extra document to the main document
                // Page collections are 1‑based; Add() works with the whole collection
                doc.Pages.Add(extra.Pages);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Document with added pages saved to '{outputPdf}'.");
    }
}