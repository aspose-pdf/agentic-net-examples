using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least six pages (1‑based indexing)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("Document contains fewer than 6 pages.");
                return;
            }

            // Retrieve page six
            Page page = doc.Pages[6];

            // Get page dimensions
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define desired watermark size (example: 200 × 100 points)
            double wmWidth = 200;
            double wmHeight = 100;

            // Compute rectangle coordinates so the watermark is centered
            double llx = (pageWidth - wmWidth) / 2;   // lower‑left X
            double lly = (pageHeight - wmHeight) / 2; // lower‑left Y
            double urx = llx + wmWidth;               // upper‑right X
            double ury = lly + wmHeight;              // upper‑right Y

            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create the watermark annotation (30 % opacity)
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.3f
            };

            // Define the visual appearance of the watermark text
            TextState textState = new TextState
            {
                FontSize = 48,
                ForegroundColor = Color.Gray
            };

            // Set the watermark text (using the API that exists in current versions)
            watermark.SetTextAndState(new string[] { "CONFIDENTIAL" }, textState);

            // Attach the annotation to the page
            page.Annotations.Add(watermark);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermark added and saved to '{outputPath}'.");
    }
}
