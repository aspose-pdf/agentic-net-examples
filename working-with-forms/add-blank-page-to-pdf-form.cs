using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_with_new_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF form inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document
            // Pages collection is 1‑based; Add() appends a new page
            pdfDoc.Pages.Add();

            // Save the modified document; existing form fields are preserved
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"New page added. Saved to '{outputPath}'.");
    }
}