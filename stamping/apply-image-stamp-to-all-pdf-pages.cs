using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImg  = "logo.png";

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

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a reusable ImageStamp
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp at the top‑right corner of each page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Top,
                Opacity = 0.5f,
                // Optional scaling
                Width  = 100,
                Height = 50
            };

            // Apply the stamp to every page using a foreach loop
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied to all pages. Saved as '{outputPdf}'.");
    }
}