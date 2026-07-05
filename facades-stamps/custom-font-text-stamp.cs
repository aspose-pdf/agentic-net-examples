using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "branded_output.pdf";
        const string fontPath = "MyBrandFont.ttf";

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
            // Load custom TrueType font
            Font customFont = FontRepository.FindFont(fontPath);

            // Configure text state with the custom font
            TextState textState = new TextState
            {
                Font = customFont,
                FontSize = 36,
                ForegroundColor = Aspose.Pdf.Color.DarkBlue
            };

            // Create a text stamp using the custom font
            TextStamp brandStamp = new TextStamp("My Brand", textState)
            {
                // Place stamp behind page content
                Background = true,
                // Position of the stamp (coordinates from bottom‑left)
                XIndent = 100,
                YIndent = 100,
                // Optional opacity for a subtle effect
                Opacity = 0.6f,
                // Center alignment on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(brandStamp);
            }

            // Save the stamped PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}