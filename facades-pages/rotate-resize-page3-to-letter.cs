using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to edit page 3:
        // - Rotate 90 degrees
        // - Change page size to Letter (8.5" x 11" = 612 x 792 points)
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Process only page 3
            editor.ProcessPages = new int[] { 3 };

            // Set rotation – cast the enum value to int because the property expects an int
            editor.Rotation = (int)Rotation.on90;

            // Set the desired output page size. The Letter size is not exposed as a
            // static member in recent Aspose.Pdf versions, so we create it manually.
            // 1 point = 1/72 inch. Letter = 8.5" x 11" => 612 x 792 points.
            editor.PageSize = new PageSize(612, 792);

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 rotated and resized. Saved to '{outputPath}'.");
    }
}
