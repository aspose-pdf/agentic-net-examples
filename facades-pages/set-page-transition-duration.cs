using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the page range to edit (example: pages 2 through 5)
        int[] pagesToEdit = { 2, 3, 4, 5 };

        // Use PdfPageEditor (facade) to modify page transition settings
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Specify which pages should be processed
            editor.ProcessPages = pagesToEdit;

            // Set the transition duration to 1 second for the selected pages
            editor.TransitionDuration = 1;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Transition duration set to 1 second on pages {string.Join(",", pagesToEdit)}. Saved to '{outputPath}'.");
    }
}