using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "stamped_output.pdf";
        const string stampImg  = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp and set precise X/Y coordinates (points)
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position measured from the lower‑left corner of the page
                XIndent = 150,                     // 150 points from the left edge
                YIndent = 300,                     // 300 points from the bottom edge
                HorizontalAlignment = HorizontalAlignment.None,
                VerticalAlignment   = VerticalAlignment.None,
                Opacity = 0.8,                     // optional transparency
                Background = false                 // draw on top of page content
            };

            // Apply the stamp to each page (or target specific pages)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save while the Document is still alive
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}