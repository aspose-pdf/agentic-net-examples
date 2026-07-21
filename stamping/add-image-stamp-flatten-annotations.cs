using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string stampImage = "stamp.png";
        const string outputPdf  = "output.pdf";

        // Verify required files exist
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

        // Load the PDF document (using rule‑based lifecycle)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the specified file
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Example visual settings (optional)
                Opacity = 0.5f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Add the stamp to the first page (Page.AddStamp)
            doc.Pages[1].AddStamp(imgStamp);

            // Flatten all annotations on every page
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards to avoid index shift after flattening
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    ann.Flatten(); // Places annotation content directly on the page and removes it
                }
            }

            // Save the modified PDF (PDF format by default)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Read‑only PDF with image stamp saved to '{outputPdf}'.");
    }
}