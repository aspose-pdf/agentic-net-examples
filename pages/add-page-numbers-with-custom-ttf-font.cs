using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and path to the custom TrueType font file
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string ttfFontPath   = "customfont.ttf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(ttfFontPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {ttfFontPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the external TrueType font and mark it for embedding
            Font customFont = FontRepository.FindFont(ttfFontPath);
            customFont.IsEmbedded = true;          // ensure the font is embedded in the output PDF

            // Create a PageNumberStamp that will be applied to each page
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            pageNumberStamp.Format = "Page # of " + pdfDoc.Pages.Count; // custom format
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20;    // distance from the bottom edge
            pageNumberStamp.StartingNumber = 1;   // start numbering from 1

            // Apply the custom font to the stamp's text state
            pageNumberStamp.TextState.Font = customFont;
            pageNumberStamp.TextState.FontSize = 12;               // desired font size
            pageNumberStamp.TextState.ForegroundColor = Color.Black; // text color

            // Add the stamp to every page in the document
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF; the custom font will be embedded automatically
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Page numbers added with custom font. Output saved to '{outputPdfPath}'.");
    }
}