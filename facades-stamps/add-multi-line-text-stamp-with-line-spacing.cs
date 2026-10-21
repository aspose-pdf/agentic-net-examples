using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, FontStyles, TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string stampText = "Confidential\nDo Not Distribute";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a multi‑line text stamp
        TextStamp stamp = new TextStamp(stampText)
        {
            // Position the stamp in the center of each page
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        // Configure text appearance – modify the existing TextState instance
        stamp.TextState.Font = FontRepository.FindFont("Arial");
        stamp.TextState.FontSize = 48;
        stamp.TextState.FontStyle = FontStyles.Bold;
        stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
        // Adjust line spacing for better readability
        stamp.TextState.LineSpacing = 1.5f;

        // Load the PDF, apply the stamp to every page, and save
        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp); // AddStamp is a Page method
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
