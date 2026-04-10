using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string footerImg = "footer.png";

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine the page width
                double pageWidth = page.PageInfo.Width;

                // Define a rectangle that spans the full page width.
                // Height is set equal to the width to ensure the image can scale
                // proportionally to the page width; AddImage will keep the aspect ratio.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageWidth);

                // Add the footer image; autoAdjustRectangle (default true) keeps the image
                // centered in the rectangle and preserves its proportions.
                page.AddImage(footerImg, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Decorative footer added and saved to '{outputPdf}'.");
    }
}