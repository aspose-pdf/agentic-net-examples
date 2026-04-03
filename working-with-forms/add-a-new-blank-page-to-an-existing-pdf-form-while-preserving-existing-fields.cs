using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";   // Existing PDF form
        const string outputPath = "output_with_blank_page.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (preserves all form fields)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document.
            // PageCollection.Add() creates an empty page using the most common page size.
            doc.Pages.Add();

            // Save the modified document. No SaveOptions needed because we are saving as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added successfully. Saved to '{outputPath}'.");
    }
}