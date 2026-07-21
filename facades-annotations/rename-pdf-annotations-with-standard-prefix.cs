using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "renamed_annotations.pdf";
        const string prefix     = "StdPrefix_";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (Facade) for batch processing
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Access the underlying Document to modify annotations
            Document doc = editor.Document;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate through annotations on the page (1‑based indexing)
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation annotation = page.Annotations[annIdx];

                    // Assign a new name with the standardized prefix
                    annotation.Name = $"{prefix}{pageNum}_{annIdx}";
                }
            }

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations renamed and saved to '{outputPath}'.");
    }
}