using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Output PDF file containing only the selected, rotated pages
        const string outputPath = "selected_rotated.pdf";

        // Pages to extract (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToExtract = new int[] { 2, 4, 5 };
        // Desired rotation for the extracted pages (must be 0, 90, 180 or 270)
        int rotationDegree = 90;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to extract and rotate pages in one step
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPath);

            // Specify which pages to edit (extract)
            editor.ProcessPages = pagesToExtract;

            // Set the rotation for those pages
            editor.Rotation = rotationDegree;

            // Apply the changes (extraction + rotation)
            editor.ApplyChanges();

            // Save the resulting PDF containing only the modified pages
            editor.Save(outputPath);
        }

        Console.WriteLine($"Created PDF with selected rotated pages: {outputPath}");
    }
}