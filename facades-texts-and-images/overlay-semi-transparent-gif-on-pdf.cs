using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp lives here

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";   // PDF that already contains a PNG
        const string outputPdf  = "output.pdf";  // Resulting PDF
        const string overlayGif = "overlay.gif"; // Semi‑transparent GIF to overlay

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayGif))
        {
            Console.Error.WriteLine($"Overlay GIF not found: {overlayGif}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block to ensure proper disposal
            using (Document doc = new Document(inputPdf))
            {
                Page page = doc.Pages[1];

                // Calculate the rectangle that covers the whole printable area of the page
                double llx = page.PageInfo.Margin.Left;
                double lly = page.PageInfo.Margin.Bottom;
                double urx = page.PageInfo.Width - page.PageInfo.Margin.Right;
                double ury = page.PageInfo.Height - page.PageInfo.Margin.Top;

                // Create an ImageStamp for the GIF and configure its appearance
                ImageStamp stamp = new ImageStamp(overlayGif)
                {
                    // Size the stamp to fill the calculated rectangle
                    Width  = urx - llx,
                    Height = ury - lly,

                    // Position the stamp – XIndent/YIndent are measured from the lower‑left corner
                    XIndent = llx,
                    YIndent = lly,

                    // Ensure the stamp is drawn over existing page content
                    Background = false,

                    // Set semi‑transparent opacity directly on the ImageStamp
                    Opacity = 0.5f
                };

                // Add the stamp to the page
                page.AddStamp(stamp);

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
