using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";
        const int    pageNumber = 1; // page to rotate (1‑based index)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to edit page properties
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Preserve any existing transition effect (do not modify TransitionType/TransitionDuration)

            // Set rotation of the specified page to 180 degrees
            // PageRotations is a dictionary where key = page number, value = rotation angle
            editor.PageRotations[pageNumber] = 180;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);

            // Close the facade (optional, using will dispose it)
            editor.Close();
        }

        Console.WriteLine($"Page {pageNumber} rotated 180° and saved to '{outputPath}'.");
    }
}