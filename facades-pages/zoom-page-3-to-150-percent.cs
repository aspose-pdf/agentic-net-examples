using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes (PdfPageEditor, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor implements IDisposable via SaveableFacade, so use a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify that only page 3 should be processed.
            editor.ProcessPages = new int[] { 3 };

            // Set zoom coefficient: 1.5 corresponds to 150% magnification.
            editor.Zoom = 1.5f;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Zoom applied to page 3. Saved as '{outputPath}'.");
    }
}