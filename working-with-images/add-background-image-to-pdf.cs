using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF and background image paths
        const string inputPdfPath   = "input.pdf";
        const string backgroundPath = "background.jpg";
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(backgroundPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate over all pages and add the background image as a stamp
                foreach (Page page in pdfDoc.Pages)
                {
                    // Create an ImageStamp bound to the background image file
                    Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(backgroundPath);

                    // Set the stamp as a background element (drawn behind page content)
                    imgStamp.Background = true;

                    // Adjust opacity for a subtle shading effect
                    imgStamp.Opacity = 0.5; // 0 = fully transparent, 1 = fully opaque

                    // Position the image – here we center it on the page.
                    // Margins are measured from the page edges; setting all to zero lets the image fill the page.
                    imgStamp.TopMargin    = 0;
                    imgStamp.BottomMargin = 0;
                    imgStamp.LeftMargin   = 0;
                    imgStamp.RightMargin  = 0;

                    // NOTE: The core Aspose.Pdf.ImageStamp class does not expose a BlendMode property.
                    // Multiply blend mode is not available via the core API; opacity is used instead.

                    // Add the stamp to the current page
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Background image added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}