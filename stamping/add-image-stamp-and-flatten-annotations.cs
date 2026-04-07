using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output_readonly.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath);
            // Optional appearance settings
            imgStamp.Opacity = 0.5;
            imgStamp.Background = false; // place stamp on top of page content
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment = VerticalAlignment.Center;

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Flatten all annotations so they become part of the page content
            foreach (Page page in doc.Pages)
            {
                // Annotation collections are 1‑based
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    ann.Flatten();
                }
            }

            // Save the resulting PDF; annotations are now flattened, making the PDF read‑only
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped and flattened PDF saved to '{outputPath}'.");
    }
}