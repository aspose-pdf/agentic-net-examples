using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerImagePath = "footer.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create an ImageStamp for the footer image
                ImageStamp footerStamp = new ImageStamp(footerImagePath);

                // Scale the image to 50 % of the page width (height adjusts to keep aspect ratio)
                footerStamp.Width = page.PageInfo.Width * 0.5;

                // Position the stamp at the bottom centre of the page
                footerStamp.HorizontalAlignment = HorizontalAlignment.Center;
                footerStamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Apply the stamp to the current page
                page.AddStamp(footerStamp);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}