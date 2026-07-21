using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string imageFilePath = "logo.png";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF and image exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp stamp = new ImageStamp(imageFilePath);

            // Set Unicode alternative text for multilingual accessibility
            stamp.AlternativeText = "示例图像 – مثال صورة – пример изображения";

            // Position the stamp (example: bottom‑right corner of the page)
            // XIndent/YIndent are measured from the bottom‑left corner of the page
            stamp.XIndent = 400; // adjust horizontal position as needed
            stamp.YIndent = 50;  // adjust vertical position as needed

            // Optional visual settings
            stamp.Opacity = 0.8f; // semi‑transparent

            // Apply the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with image stamp saved to '{outputPdfPath}'.");
    }
}