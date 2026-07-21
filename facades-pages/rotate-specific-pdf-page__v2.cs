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

        // PdfPageEditor is a facade for page-level editing.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file into the editor.
            editor.BindPdf(inputPath);

            // Restrict editing to page 4 only.
            editor.ProcessPages = new int[] { 4 };

            // Set rotation to 180 degrees for the selected page(s).
            editor.Rotation = 180;

            // Apply the rotation change.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated 180° and saved to '{outputPath}'.");
    }
}