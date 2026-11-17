using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string logoPath  = "logo.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Create an image stamp from the logo file
                ImageStamp logoStamp = new ImageStamp(logoPath);
                // Set semi‑transparent opacity (0 = fully transparent, 1 = opaque)
                logoStamp.Opacity = 0.5;
                // Position the stamp – example: bottom‑right corner of the page
                logoStamp.HorizontalAlignment = HorizontalAlignment.Right;
                logoStamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Pages are 1‑based; get the first page and add the stamp
                Page firstPage = doc.Pages[1];
                firstPage.AddStamp(logoStamp);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Logo stamp added to first page. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}