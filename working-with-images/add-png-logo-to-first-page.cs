using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string logoPng   = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPng))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPng}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based; get the first page
            Page firstPage = doc.Pages[1];

            // Create an ImageStamp for the PNG logo
            ImageStamp logoStamp = new ImageStamp(logoPng)
            {
                // Position the lower‑left corner of the image
                XIndent = 50,   // distance from the left edge of the page
                YIndent = 750,  // distance from the bottom edge of the page
                // Optional: set explicit size (otherwise original image size is used)
                // Width = 120,
                // Height = 60,
                // Place the image above existing content
                Background = false
            };

            // Add the stamp to the first page
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Logo added and saved to '{outputPdf}'.");
    }
}