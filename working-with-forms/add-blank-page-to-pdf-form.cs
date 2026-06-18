using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_with_blank_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF form
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            // Page collection uses 1‑based indexing; Add() appends a page.
            doc.Pages.Add();

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added. Saved to '{outputPath}'.");
    }
}