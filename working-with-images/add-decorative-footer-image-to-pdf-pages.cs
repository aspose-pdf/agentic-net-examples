using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // required for ImageStamp, alignment enums

// The class name matches the executable name expected by the test harness.
class AsposePdfApi_2f71103e
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string footerImagePath = "footer.png";
        const string outputPdfPath = "output_with_footer.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Create a new ImageStamp for each page
                ImageStamp footerStamp = new ImageStamp(footerImagePath)
                {
                    // Scale the image to match the page width; height is adjusted automatically
                    Width = page.PageInfo.Width,
                    // Height = 0 lets Aspose.Pdf preserve the original aspect ratio
                    Height = 0,

                    // Align the stamp to the bottom centre of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,

                    // Optional margin from the bottom edge (in points)
                    BottomMargin = 10
                };

                // Add the stamp to the current page
                page.AddStamp(footerStamp);
            }

            // Save the modified PDF (still inside the using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Footer image added to each page. Saved as '{outputPdfPath}'.");
    }
}
