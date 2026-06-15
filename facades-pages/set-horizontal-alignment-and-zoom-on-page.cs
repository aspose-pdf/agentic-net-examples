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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Target only page 4 (Aspose.Pdf uses 1‑based indexing)
            editor.ProcessPages = new int[] { 4 };

            // Set horizontal alignment to Center and zoom to 1.2 (120%)
            editor.HorizontalAlignment = HorizontalAlignment.Center;
            editor.Zoom = 1.2f;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}