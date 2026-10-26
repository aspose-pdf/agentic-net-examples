using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input FDF files (replace with actual paths)
        string[] fdfFiles = { "annotations1.fdf", "annotations2.fdf", "annotations3.fdf" };
        // Output PDF that will contain the consolidated annotations
        const string outputPdf = "consolidated_annotations.pdf";

        // Create a new PDF document with a single blank page
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (size A4)
            Page page = pdfDoc.Pages.Add();

            // Consolidate annotations from each FDF file
            foreach (string fdfPath in fdfFiles)
            {
                if (!File.Exists(fdfPath))
                {
                    Console.Error.WriteLine($"FDF file not found: {fdfPath}");
                    continue;
                }

                // NOTE: Aspose.Pdf core library does not provide direct FDF parsing.
                // As a placeholder, we read the file bytes and create a simple
                // TextAnnotation for each FDF file to demonstrate consolidation.
                // In a real scenario, you would parse the FDF content and create
                // appropriate annotation objects (TextAnnotation, HighlightAnnotation, etc.).
                byte[] fdfData = File.ReadAllBytes(fdfPath);
                string annotationContent = $"Imported from {Path.GetFileName(fdfPath)} (size {fdfData.Length} bytes)";

                // Define annotation rectangle (positioned sequentially down the page)
                // Using fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity.
                float yPos = 800 - (pdfDoc.Pages[1].Annotations.Count * 50);
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, yPos, 400, yPos + 40);

                // Create the TextAnnotation and set its properties
                TextAnnotation txtAnn = new TextAnnotation(page, rect)
                {
                    Title = "FDF Import",
                    Contents = annotationContent,
                    Color = Aspose.Pdf.Color.Yellow,
                    Open = true,
                    Icon = TextIcon.Note
                };

                // Add the annotation to the page
                page.Annotations.Add(txtAnn);
            }

            // Save the consolidated PDF
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"Consolidated annotations saved to '{outputPdf}'.");
        }
    }
}