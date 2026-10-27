using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Annotations;        // For annotation types if needed (not used here)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output_accessible.pdf";

        // Verify files exist
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the PNG file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp (example: bottom‑right corner)
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional scaling – use Width/Height or omit to keep original size
                // ImageScale property is not available in the current Aspose.Pdf version.
                // Alternative: set Width/Height manually if scaling is required.
                // Set Unicode alternative text for multilingual accessibility
                // This text will be embedded in the PDF's /Alt entry for the image
                AlternativeText = "示例图像 – Example Image – مثال صورة"
            };

            // If you need to specify an explicit rectangle, use the fully qualified type
            // Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 300);
            // imgStamp.Rectangle = rect;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (no special SaveOptions needed for PDF output)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with image stamp and Unicode alt text: {outputPdf}");
    }
}
