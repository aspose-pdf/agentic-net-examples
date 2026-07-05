using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "readOnlyStamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF document (using the required lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Optional visual settings
                Background = false,          // stamp appears on top of page content
                Opacity    = 0.5f,           // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Add the stamp to the first page (pages are 1‑based)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Flatten all annotations on every page
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];
                // Annotation collections are also 1‑based
                for (int a = 1; a <= page.Annotations.Count; a++)
                {
                    Annotation ann = page.Annotations[a];
                    ann.Flatten(); // places annotation content directly on the page and removes the annotation object
                }
            }

            // Save the modified PDF (using the required lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped and flattened PDF saved to '{outputPath}'.");
    }
}