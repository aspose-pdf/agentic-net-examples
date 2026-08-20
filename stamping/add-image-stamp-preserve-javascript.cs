using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Annotations;   // For StampAnnotation if needed (not used here)

class Program
{
    static void Main()
    {
        // Input PDF that may contain JavaScript actions
        const string inputPdf  = "input.pdf";
        // Image to be used as a stamp (logo, watermark, etc.)
        const string stampImage = "stamp.png";
        // Output PDF – JavaScript actions are preserved automatically
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp instance.
            // The stamp can be reused for all pages; properties are applied per page.
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Example visual settings – adjust as needed
                Background = false,          // Stamp appears on top of page content
                Opacity   = 0.5,             // Semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Position can also be set via XIndent/YIndent or margins
                // Here we let alignment handle placement.
            };

            // Apply the stamp to every page.
            // Page.AddStamp adds the stamp without affecting existing annotations,
            // form fields, or JavaScript actions.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF. Existing JavaScript actions remain intact.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}