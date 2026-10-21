using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, Font, Color

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "branded_output.pdf";
        const string fontFilePath = "CustomBrandFont.ttf"; // TrueType font file

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fontFilePath))
        {
            Console.Error.WriteLine($"Font file not found: {fontFilePath}");
            return;
        }

        try
        {
            // Load the custom TrueType font
            Font customFont = FontRepository.OpenFont(fontFilePath);

            // Create a text stamp with branding text
            TextStamp brandStamp = new TextStamp("My Brand");
            brandStamp.TextState.Font = customFont;                 // apply custom font
            brandStamp.TextState.FontSize = 36;                     // size of the text
            brandStamp.TextState.ForegroundColor = Color.FromRgb(0, 0.5, 1); // brand color
            brandStamp.HorizontalAlignment = HorizontalAlignment.Center;
            brandStamp.VerticalAlignment = VerticalAlignment.Bottom;
            // Position 20 points above the bottom edge
            brandStamp.YIndent = 20; // for Bottom alignment this is distance from bottom
            // XIndent is not needed when using Center alignment

            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Apply the stamp to every page
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(brandStamp);
                }

                // Save the stamped PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
