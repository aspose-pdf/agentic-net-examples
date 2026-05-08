using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "form_stamped.pdf";
        const string stampImagePath = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF containing form fields
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            // Position the stamp (example: top‑right corner with a margin)
            imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
            imgStamp.VerticalAlignment   = VerticalAlignment.Top;
            // ImageStamp does not have a Margin property – use XIndent/YIndent instead
            imgStamp.XIndent = 10; // 10 points from the right edge (when Right‑aligned)
            imgStamp.YIndent = 10; // 10 points from the top edge
            imgStamp.Opacity = 0.5; // semi‑transparent

            // Apply the stamp to each page; form fields stay functional
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
