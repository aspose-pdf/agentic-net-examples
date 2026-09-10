using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create the editor, bind the source PDF, apply a change, and save.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);   // Load the PDF into the facade.

                // Example modification: set zoom to 75%.
                editor.Zoom = 0.75f;

                // Apply the pending changes.
                editor.ApplyChanges();

                // Save the edited document to a new file.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}