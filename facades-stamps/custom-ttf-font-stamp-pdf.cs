using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "branded_output.pdf";
        const string ttfPath   = "BrandFont.ttf";   // path to the custom TrueType font
        const string stampText = "My Unique Brand";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(ttfPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {ttfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Load the custom TrueType font from a stream (Aspose expects a Stream, not a file path)
            using (FileStream fontStream = File.OpenRead(ttfPath))
            {
                Font customFont = FontRepository.OpenFont(fontStream, FontTypes.TTF);

                // Configure the text state with the custom font and desired appearance
                TextState textState = new TextState
                {
                    Font = customFont,
                    FontSize = 36,
                    ForegroundColor = Aspose.Pdf.Color.Blue
                };

                // Create a text stamp using the custom text state
                TextStamp brandStamp = new TextStamp(stampText, textState)
                {
                    // Position the stamp at the bottom‑center of each page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    YIndent = 20,               // distance from the bottom edge
                    Opacity = 0.8f,             // semi‑transparent
                    Background = false          // draw on top of page content
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(brandStamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
