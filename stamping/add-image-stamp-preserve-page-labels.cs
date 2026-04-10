using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "logo.png";
        const string outputPdf = "stamped_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp 50 points from the left and 50 points from the bottom
                XIndent = 50,
                YIndent = 50,
                // Optional visual settings
                Opacity = 0.5,
                // Keep the stamp on top of page content
                Background = false
            };

            // Apply the same stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // No need to reassign PageLabels – the property is read‑only and stamping does not modify them.

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added. Output saved to '{outputPdf}'.");
    }
}
