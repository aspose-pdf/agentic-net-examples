using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for input MHT file and output PDF
        const string mhtPath = "input.mht";
        const string pdfPath = "output.pdf";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        MhtLoadOptions loadOptions = new MhtLoadOptions();
        using (Document doc = new Document(mhtPath, loadOptions))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the watermark annotation will appear
            // (left, bottom, width, height) – adjust as needed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 100);

            // Create the WatermarkAnnotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect)
            {
                // Set the annotation color (optional, affects border if used)
                Color = Aspose.Pdf.Color.LightGray,
                // Set opacity (0 = fully transparent, 1 = fully opaque)
                Opacity = 0.5
            };

            // Define the visual style of the watermark text
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 36,
                ForegroundColor = Aspose.Pdf.Color.Red,
                // Optional: make the text semi‑transparent via opacity
                // Note: Opacity is also controlled by the annotation itself
            };

            // Set the watermark text (single line in this example)
            watermark.SetTextAndState(new[] { "CONFIDENTIAL" }, textState);

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(watermark);

            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{pdfPath}'.");
    }
}