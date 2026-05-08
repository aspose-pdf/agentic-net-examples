using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for Annotation type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

        // Verify input files exist
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

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Add an image stamp to each page
                foreach (Page page in doc.Pages)
                {
                    // Create ImageStamp from file
                    ImageStamp imgStamp = new ImageStamp(stampImagePath);
                    // Optional appearance settings
                    imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    imgStamp.VerticalAlignment   = VerticalAlignment.Center;
                    imgStamp.Opacity = 0.5; // semi‑transparent

                    // Apply stamp to the page (Page.AddStamp)
                    page.AddStamp(imgStamp);
                }

                // Flatten all annotations so they become part of the page content
                foreach (Page page in doc.Pages)
                {
                    // Iterate over annotations; collection is 1‑based but foreach works
                    foreach (Annotation ann in page.Annotations)
                    {
                        ann.Flatten(); // places annotation content directly on the page
                    }
                }

                // Save the modified PDF (lifecycle rule: Document.Save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Read‑only PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}