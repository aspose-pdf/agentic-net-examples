using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "stamped_output.pdf";
        const string stampImage = "stamp.png";

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

        try
        {
            // Load the source PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Create an image stamp from the specified image file.
                ImageStamp imgStamp = new ImageStamp(stampImage)
                {
                    // Position the stamp at the center of each page.
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Make the stamp semi‑transparent.
                    Opacity = 0.5,
                    // Ensure the stamp is drawn on top of page content.
                    Background = false
                };

                // Apply the stamp to every page in the document.
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF. No SaveOptions needed because the output format is PDF.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}