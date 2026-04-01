using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fontPath = "custom.ttf";
        const string stampText = "MyBrand";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"TrueType font not found: {fontPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Configure text state with the custom font
            TextState textState = new TextState();
            textState.Font = FontRepository.FindFont(fontPath);
            textState.FontSize = 36;
            textState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create a text stamp using the custom font
            TextStamp textStamp = new TextStamp(stampText, textState);
            textStamp.Background = false; // draw on top of page content
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;
            textStamp.Opacity = 0.5f;

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the stamped PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved as '{outputPath}'.");
    }
}