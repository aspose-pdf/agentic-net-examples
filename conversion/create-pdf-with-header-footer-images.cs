using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // not needed but harmless; ensures all Aspose.Pdf types are available

class Program
{
    static void Main()
    {
        const string headerPath = "header.png";
        const string footerPath = "footer.jpg";
        const string outputPath = "twoimages.pdf";

        // Verify that the image files exist before proceeding
        if (!File.Exists(headerPath))
        {
            Console.Error.WriteLine($"Header image not found: {headerPath}");
            return;
        }
        if (!File.Exists(footerPath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerPath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdf = new Document())
        {
            // Add a new blank page (Aspose.Pdf creates one by default, but we add explicitly)
            Page page = pdf.Pages.Add();

            // ----- Add header image -----
            using (FileStream headerStream = File.OpenRead(headerPath))
            {
                Image headerImg = new Image
                {
                    ImageStream = headerStream,
                    // Align the image to the top of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Optional: set a margin if needed
                    Margin = new MarginInfo { Top = 10, Bottom = 0, Left = 0, Right = 0 }
                };
                page.Paragraphs.Add(headerImg);
            }

            // ----- Add footer image -----
            using (FileStream footerStream = File.OpenRead(footerPath))
            {
                Image footerImg = new Image
                {
                    ImageStream = footerStream,
                    // Align the image to the bottom of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    Margin = new MarginInfo { Bottom = 10, Top = 0, Left = 0, Right = 0 }
                };
                page.Paragraphs.Add(footerImg);
            }

            // Save the document as a PDF file
            pdf.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
    }
}