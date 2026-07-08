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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF that contains form fields.
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp from the specified image file.
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Position the stamp in the top‑right corner with a small margin.
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Top,
                // Use XIndent/YIndent instead of the non‑existent Margin property.
                XIndent = 10,
                YIndent = 10,
                // Make the stamp semi‑transparent so underlying form fields stay visible and usable.
                Opacity = 0.5f
            };

            // Apply the stamp to every page. Form fields remain functional because
            // ImageStamp does not flatten or modify the form annotations.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the resulting PDF. The document is disposed automatically by the using block.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
