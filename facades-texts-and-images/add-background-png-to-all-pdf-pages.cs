using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_background.pdf";
        const string backgroundPng = "background.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundPng))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPng}");
            return;
        }

        // Load the PDF document
        using (Document pdf = new Document(inputPdfPath))
        {
            // Iterate over each page and add the background image as a stamp
            for (int i = 1; i <= pdf.Pages.Count; i++)
            {
                Page page = pdf.Pages[i];
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create an ImageStamp that covers the whole page
                ImageStamp imgStamp = new ImageStamp(backgroundPng)
                {
                    // Set the stamp size to match the page size
                    Width = pageWidth,
                    Height = pageHeight,
                    // Position at the lower‑left corner of the page
                    LeftMargin = 0,
                    BottomMargin = 0,
                    // Place the stamp behind existing content
                    Background = true
                };

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdf.Save(outputPdfPath);
        }

        Console.WriteLine($"Background image added to all pages. Output saved to '{outputPdfPath}'.");
    }
}
