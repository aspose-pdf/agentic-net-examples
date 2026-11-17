using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Required for flattening annotations

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImg = "stamp.png";
        const string outputPdf = "readOnlyStamped.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp instance.
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp at the bottom‑right corner of each page.
                // Coordinates are in points (1/72 inch). Adjust as needed.
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                // Optional visual settings.
                Opacity = 0.5,
                Background = false
            };

            // Apply the stamp to every page individually.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Flatten all annotations so they become part of the page content.
            // Use PdfAnnotationEditor from Aspose.Pdf.Facades.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);
                editor.FlatteningAnnotations();
            }

            // Save the resulting PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Read‑only PDF with image stamp saved to '{outputPdf}'.");
    }
}
