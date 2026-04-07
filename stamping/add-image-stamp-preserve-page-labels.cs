using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp (inherits from Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "stamped_output.pdf";
        const string stampImg  = "logo.png";

        if (!File.Exists(inputPdf) || !File.Exists(stampImg))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Apply the image stamp to each page. Stamping does not modify page labels,
            // so we do not need to copy/restore them explicitly.
            foreach (Page page in doc.Pages)
            {
                ImageStamp imgStamp = new ImageStamp(stampImg)
                {
                    Background = false,               // stamp on top of content
                    Opacity = 0.5,                    // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}
