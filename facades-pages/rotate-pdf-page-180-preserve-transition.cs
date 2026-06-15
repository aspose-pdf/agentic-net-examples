using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (facade) to edit page properties.
        // The editor implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Rotate page 1 by 180 degrees.
            // Using the PageRotations dictionary preserves any existing
            // transition effect (TransitionType / TransitionDuration) on the page.
            editor.PageRotations[1] = 180;

            // Apply the pending changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page rotated and saved to '{outputPath}'.");
    }
}