using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for FontStyles enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a text stamp with desired appearance
            TextStamp textStamp = new TextStamp("Confidential");
            textStamp.Opacity = 0.6; // subtle opacity
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment = VerticalAlignment.Center;
            textStamp.TextState.FontSize = 72;
            textStamp.TextState.FontStyle = FontStyles.Bold; // Fixed: FontStyles now resolved
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(1, 0, 0); // optional red color

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Text stamp added and saved to '{outputPath}'.");
    }
}
