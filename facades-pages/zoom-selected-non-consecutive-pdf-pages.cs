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

        // Define the non‑consecutive pages (1‑based) to which the zoom will be applied
        int[] pagesToEdit = new int[] { 1, 3, 5 };
        // Desired zoom factor (1.0 = 100%)
        double zoomFactor = 1.5; // 150%

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Specify which pages to process
            editor.ProcessPages = pagesToEdit;

            // Apply a common zoom factor to the selected pages
            // PdfPageEditor.Zoom expects an integer representing the percentage (e.g., 150 for 150%).
            editor.Zoom = (int)(zoomFactor * 100);

            // Commit the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages {string.Join(",", pagesToEdit)} zoomed to {zoomFactor * 100}% and saved to '{outputPath}'.");
    }
}
