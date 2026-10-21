using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string logoImage = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoImage))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImage}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages.
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF does not contain a third page.");
                return;
            }

            // Create an image stamp from the logo file.
            ImageStamp logoStamp = new ImageStamp(logoImage)
            {
                // Align the stamp to the bottom‑right corner.
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional margins from the page edges.
                BottomMargin = 10,
                RightMargin  = 10
            };

            // Add the stamp to page 3 using the high‑level Document API.
            doc.Pages[3].AddStamp(logoStamp);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp applied and saved to '{outputPdf}'.");
    }
}
