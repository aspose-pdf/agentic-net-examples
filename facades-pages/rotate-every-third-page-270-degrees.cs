using System;
using System.Collections;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PdfPageEditor facade and bind the source PDF.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);

            // Retrieve total number of pages (1‑based indexing).
            int pageCount = editor.GetPages();

            // Rotate every third page by 270 degrees.
            for (int i = 1; i <= pageCount; i++)
            {
                if (i % 3 == 0)
                {
                    // PageRotations is a Hashtable: key = page number, value = rotation angle.
                    editor.PageRotations[i] = 270;
                }
            }

            // Apply the rotation changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}