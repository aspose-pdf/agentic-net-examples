using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Multi‑line disclaimer text (use \n for line breaks)
        const string disclaimer = "Disclaimer:\nThis document is confidential.\nDo not distribute without permission.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the disclaimer text
            TextStamp textStamp = new TextStamp(disclaimer)
            {
                // Position the stamp at the bottom‑center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Enable a semi‑transparent background rectangle
                Background = true,
                Opacity = 0.8f
            };

            // TextState is read‑only – modify the existing instance instead of assigning a new one
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 12;
            textStamp.TextState.ForegroundColor = Color.Black;
            // Custom line spacing (e.g., 1.5 times the font size)
            textStamp.TextState.LineSpacing = 1.5f;

            // Optional: set a background colour if the property exists in the used version
            // textStamp.BackgroundColor = Color.FromRgb(0.9, 0.9, 0.9);

            // Apply the stamp to every page (AddStamp is a Page method)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
