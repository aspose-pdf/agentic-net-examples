using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for annotation handling (flattening)

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an image stamp and configure its appearance
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                Background          = false,
                Opacity             = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page (AddStamp is a Page method, not a collection method)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Flatten all annotations so they become part of the page content and cannot be edited
            foreach (Page page in pdfDoc.Pages)
            {
                // Copy annotations to a list to avoid modifying the collection while iterating
                var annotations = new List<Annotation>(page.Annotations);
                foreach (var annotation in annotations)
                {
                    annotation.Flatten();
                }
            }

            // Save the modified PDF as a read‑only document
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped and read‑only PDF saved to '{outputPath}'.");
    }
}
