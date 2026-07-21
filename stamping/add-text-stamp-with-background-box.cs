using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired text
            TextStamp stamp = new TextStamp("CONFIDENTIAL");

            // Configure text appearance
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 24;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.White;          // Text color
            stamp.TextState.BackgroundColor = Aspose.Pdf.Color.Black;          // Background box color
            stamp.TextState.FontStyle = FontStyles.Bold;

            // Position the stamp in the center of each page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Optional: make the stamp semi‑transparent
            stamp.Opacity = 0.6f;

            // Add the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with text stamp: {outputPath}");
    }
}