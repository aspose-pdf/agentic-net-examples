using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png";

        // Verify input files exist
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

        // Load the PDF document (disposed automatically)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp and configure its appearance
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                Background = false,                     // Stamp on top of page content
                Opacity = 0.5,                          // 50% transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the stamp to every page using a foreach loop
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (writes PDF regardless of extension)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied to all pages. Saved as '{outputPdf}'.");
    }
}