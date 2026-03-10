using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        // Load the CGM file (input‑only format) and convert it to a PDF document
        using (Document doc = new Document(inputCgm, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page after conversion
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("No pages were generated from the CGM file.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the watermark annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the watermark annotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                Opacity = 0.5,                         // semi‑transparent
                Color = Aspose.Pdf.Color.LightGray,   // annotation color
                Contents = "Sample Watermark"          // optional text
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(watermark);

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermark annotation added and saved to '{outputPdf}'.");
    }
}