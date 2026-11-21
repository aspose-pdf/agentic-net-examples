using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an image stamp from the specified image file
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Position the stamp in the centre of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Ensure the stamp appears on top of existing content
                Background = false
            };

            // Apply the stamp only to the second page (pages are 1‑based)
            if (pdfDoc.Pages.Count >= 2)
            {
                Page secondPage = pdfDoc.Pages[2];
                secondPage.AddStamp(imgStamp);
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain a second page.");
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Stamp added to page 2 and saved as '{outputPdf}'.");
    }
}
