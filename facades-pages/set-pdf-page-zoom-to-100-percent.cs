using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor is a facade for page-level modifications.
        // It implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Set the zoom coefficient to 1.0 (100% scaling) for all pages.
            editor.Zoom = 1.0f;

            // Apply the changes made to the document.
            editor.ApplyChanges();

            // Save the modified PDF to the desired output file.
            editor.Save(outputPath);

            // Close the editor (optional, as using will dispose it).
            editor.Close();
        }

        Console.WriteLine($"Zoom set to 1.0 and saved to '{outputPath}'.");
    }
}