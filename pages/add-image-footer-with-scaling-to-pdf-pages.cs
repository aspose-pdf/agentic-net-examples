using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_footer.pdf";
        const string footerImg = "footer.png"; // path to the footer image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(footerImg))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImg}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create an Image object for the footer
                Image img = new Image
                {
                    File = footerImg,                 // load image file
                    ImageScale = 0.5,                // scale to 50 % of original size
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                // Add the image to the page's paragraph collection.
                // This places the image according to the alignment settings.
                page.Paragraphs.Add(img);
            }

            // Save the modified PDF. No SaveOptions needed for PDF output.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPdf}'.");
    }
}