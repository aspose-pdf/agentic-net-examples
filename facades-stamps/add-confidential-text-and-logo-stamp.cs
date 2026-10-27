using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "stamped_output.pdf";
        const string logoImagePath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // ------------------------------------------------------------
                // 1. Image stamp – company logo
                // ------------------------------------------------------------
                ImageStamp logoStamp = new ImageStamp(logoImagePath)
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment   = VerticalAlignment.Top,
                    XIndent = 20,   // distance from left edge
                    YIndent = 20,   // distance from top edge
                    Background = false,
                    Opacity = 0.8
                };

                // ------------------------------------------------------------
                // 2. Text stamp – the word "Confidential"
                // ------------------------------------------------------------
                TextStamp textStamp = new TextStamp("Confidential")
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Background = false,
                    Opacity = 0.5
                };
                // All visual properties of a TextStamp are set through its TextState object
                textStamp.TextState.Font = FontRepository.FindFont("Arial");
                textStamp.TextState.FontSize = 24;
                textStamp.TextState.FontStyle = FontStyles.Bold;
                textStamp.TextState.ForegroundColor = Color.Red;

                // ------------------------------------------------------------
                // 3. Apply both stamps to every page in the document
                // ------------------------------------------------------------
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(logoStamp);
                    page.AddStamp(textStamp);
                }

                // Save the stamped PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
