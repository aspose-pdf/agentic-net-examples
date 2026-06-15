using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "branded_output.pdf";
        const string fontPath  = "custom.ttf"; // TrueType font file for branding

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Load the custom TrueType font
            Font customFont = FontRepository.FindFont(fontPath);

            // Configure text appearance using TextState
            TextState textState = new TextState
            {
                Font = customFont,                     // custom font
                FontSize = 48,                         // desired size
                ForegroundColor = Aspose.Pdf.Color.Blue // text color
            };

            // Create a TextStamp with the branding text and the custom TextState
            TextStamp brandStamp = new TextStamp("MyBrand", textState)
            {
                // Position the stamp on the page (coordinates are from bottom‑left)
                XIndent = 100,   // horizontal position
                YIndent = 700,   // vertical position

                // Optional visual settings
                Opacity = 0.85f, // semi‑transparent
                Background = false // draw on top of page content
            };

            // Add the stamp to the first page (1‑based indexing)
            Page firstPage = doc.Pages[1];
            firstPage.AddStamp(brandStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branded PDF saved to '{outputPdf}'.");
    }
}