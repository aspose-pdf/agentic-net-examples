using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to edit page transitions
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Specify the pages to be edited (example: pages 2 through 5)
            editor.ProcessPages = new int[] { 2, 3, 4, 5 };

            // Set transition duration to 1 second for the selected pages
            editor.TransitionDuration = 1;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}