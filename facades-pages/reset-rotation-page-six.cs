using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_reset_rotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the editor, reset rotation of page 6, and save.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Verify the document has at least six pages (1‑based indexing).
            if (editor.GetPages() < 6)
            {
                Console.Error.WriteLine("The document contains fewer than 6 pages.");
                return;
            }

            // Reset rotation of page 6 to zero degrees.
            editor.PageRotations[6] = 0;

            // Apply the changes and save the result.
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotation of page 6 reset. Saved to '{outputPath}'.");
    }
}