using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (which may contain form fields)
        using (Document doc = new Document(inputPath))
        {
            // Add a new blank page at the end of the document.
            // Page collection uses 1‑based indexing; Add() creates an empty page.
            doc.Pages.Add();

            // Save the updated PDF. No SaveOptions are required for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page added and saved to '{outputPath}'.");
    }
}