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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor facade to modify page sizes
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Select pages 3 through 6 (Aspose.Pdf uses 1‑based indexing)
            editor.ProcessPages = new int[] { 3, 4, 5, 6 };

            // Set the target page size to US Letter (8.5" x 11" = 612 x 792 points)
            // The PageSize enum may not expose a "Letter" member in some library versions,
            // so we create the size explicitly.
            editor.PageSize = new PageSize(612, 792);

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Pages 3‑6 resized to Letter and saved as '{outputPath}'.");
    }
}
