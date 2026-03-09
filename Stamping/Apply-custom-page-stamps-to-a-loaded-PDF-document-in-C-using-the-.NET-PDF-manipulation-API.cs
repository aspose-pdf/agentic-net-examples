using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Text;                // For TextStamp and TextState
using Aspose.Pdf.Annotations;         // Optional, not used directly here

class Program
{
    static void Main()
    {
        // Input PDF to be stamped
        const string inputPdfPath = "input.pdf";
        // Optional PDF whose page will be used as a stamp
        const string stampPdfPath = "stamp_source.pdf";
        // Output PDF with applied stamps
        const string outputPdfPath = "stamped_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampPdfPath))
        {
            Console.Error.WriteLine($"Stamp source file not found: {stampPdfPath}");
            return;
        }

        // Load the main document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Load the PDF that provides a page to be used as a stamp
            using (Document stampDoc = new Document(stampPdfPath))
            {
                // Retrieve the first page of the stamp source document (1‑based indexing)
                Page stampSourcePage = stampDoc.Pages[1];

                // Create a PdfPageStamp that will embed the entire page as a stamp
                PdfPageStamp pdfPageStamp = new PdfPageStamp(stampSourcePage)
                {
                    // Example customizations
                    Background = false,                 // Stamp on top of existing content
                    Opacity = 0.5,                      // Semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    // Scale the stamp to 50 % of its original size
                    Zoom = 0.5
                };

                // Create an ImageStamp (e.g., a logo) that will be placed on each page
                ImageStamp imageStamp = new ImageStamp("logo.png")
                {
                    // Position the image at the bottom‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    // Add a small margin from the edges
                    RightMargin = 20,
                    BottomMargin = 20,
                    // Make the image slightly transparent
                    Opacity = 0.8,
                    // Keep the original size (no scaling)
                    Zoom = 1.0
                };

                // Create a TextStamp with custom text
                TextStamp textStamp = new TextStamp("Confidential")
                {
                    // Rotate the text 45 degrees
                    RotateAngle = 45,
                    // Center the text on the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    // Make the text semi‑transparent
                    Opacity = 0.3
                };
                // Configure the TextState (read‑only property) after construction
                textStamp.TextState.FontSize = 48;
                textStamp.TextState.FontStyle = FontStyles.Bold;
                textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                // Create a PageNumberStamp to add page numbers
                PageNumberStamp pageNumberStamp = new PageNumberStamp("#")
                {
                    // Place numbers at the bottom‑center of each page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    BottomMargin = 15
                };
                // Configure its TextState after construction
                pageNumberStamp.TextState.FontSize = 12;
                pageNumberStamp.TextState.FontStyle = FontStyles.Regular;
                pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

                // Iterate over all pages (1‑based indexing) and apply the stamps
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Add the PDF page stamp
                    page.AddStamp(pdfPageStamp);

                    // Add the image stamp
                    page.AddStamp(imageStamp);

                    // Add the text stamp
                    page.AddStamp(textStamp);

                    // Add the page number stamp
                    page.AddStamp(pageNumberStamp);
                }
            }

            // Save the modified document as PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamps applied successfully. Output saved to '{outputPdfPath}'.");
    }
}
