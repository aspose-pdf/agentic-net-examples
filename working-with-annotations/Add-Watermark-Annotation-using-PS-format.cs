using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPsPath  = "input.ps";      // PS file to load
        const string outputPdfPath = "watermarked.pdf";

        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"File not found: {inputPsPath}");
            return;
        }

        // Load the PostScript file (PS is input‑only in Aspose.Pdf)
        using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the watermark annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the WatermarkAnnotation
            WatermarkAnnotation watermark = new WatermarkAnnotation(page, rect);

            // Prepare the text state (font, size, color)
            TextState textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.Red,
                FontStyle = FontStyles.Bold
            };

            // Set the watermark text (can be multiple lines)
            string[] watermarkLines = new string[] { "CONFIDENTIAL" };
            watermark.SetTextAndState(watermarkLines, textState);

            // Add the annotation to the page
            page.Annotations.Add(watermark);

            // Save the result as PDF (PS cannot be used as an output format)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}