using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string stampImage = "logo.png";

        // Unicode alternative text (e.g., Chinese, English, Arabic)
        const string altText = "示例图像 – Example image – مثال صورة";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from a file
            ImageStamp imgStamp = new ImageStamp(stampImage);

            // Set Unicode alternative text for accessibility
            imgStamp.AlternativeText = altText;

            // Optional visual settings (centered, semi‑transparent)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;
            imgStamp.Opacity = 0.5; // 0.0 (transparent) to 1.0 (opaque)

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp with Unicode alt text saved to '{outputPdf}'.");
    }
}