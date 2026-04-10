using System;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (a SaveableFacade) to edit page rotation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPath);

            // Set rotation for page 4 to 180 degrees.
            // PageRotations is a dictionary where key = page number (1‑based) and value = rotation angle.
            editor.PageRotations = new Dictionary<int, int>
            {
                { 4, 180 }
            };

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated to 180° and saved as '{outputPath}'.");
    }
}