using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to rotate pages based on orientation
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF file
            editor.BindPdf(inputPath);

            // Prepare a dictionary for page rotations (page number -> rotation angle)
            Dictionary<int, int> rotations = new Dictionary<int, int>();

            // Get total number of pages (1‑based indexing)
            int pageCount = editor.GetPages();

            // Examine each page size and set rotation for landscape pages
            for (int i = 1; i <= pageCount; i++)
            {
                PageSize size = editor.GetPageSize(i);
                // Landscape detection: width greater than height
                if (size.Width > size.Height)
                {
                    // Rotate 90 degrees to portrait orientation
                    rotations[i] = 90; // valid values: 0, 90, 180, 270
                }
            }

            // Apply the rotation settings
            editor.PageRotations = rotations;

            // Commit changes and save the output PDF
            editor.ApplyChanges();
            editor.Save(outputPath);
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
