using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string headerImg = "header.png"; // decorative header image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(headerImg))
        {
            Console.Error.WriteLine($"Header image not found: {headerImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Determine page height to position the header at the top
                double pageHeight = page.PageInfo.Height;

                // Desired header height (in points) and left margin
                double headerHeight = 80;   // adjust as needed
                double leftMargin   = 0;
                double rightMargin  = page.PageInfo.Width; // full width

                // Rectangle for the header image:
                // llx, lly, urx, ury (lower‑left and upper‑right corners)
                // Place it just below the top edge, leaving a small top margin (e.g., 10 pts)
                double topMargin = 10;
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    leftMargin,
                    pageHeight - headerHeight - topMargin,
                    rightMargin,
                    pageHeight - topMargin);

                // Add the image; it will be centered within the rectangle preserving aspect ratio
                page.AddImage(headerImg, rect);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Header image added to each page. Saved as '{outputPdf}'.");
    }
}