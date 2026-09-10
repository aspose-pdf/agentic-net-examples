using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string stampImage = "stamp.png";          // image to use as stamp
        const string outputPdf  = "readOnlyStamped.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Add the image stamp to every page
            foreach (Page page in doc.Pages)
            {
                ImageStamp imgStamp = new ImageStamp(stampImage)
                {
                    // Example visual settings (optional)
                    Background          = false,
                    Opacity             = 0.5f,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center
                };

                // Page.AddStamp places the stamp on the page
                page.AddStamp(imgStamp);
            }

            // Flatten all annotations so they become part of the page content
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards because Flatten removes the annotation from the collection
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    ann.Flatten(); // Places annotation content directly on the page and removes it
                }
            }

            // Save the modified PDF (read‑only because annotations are flattened)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved as read‑only file: {outputPdf}");
    }
}