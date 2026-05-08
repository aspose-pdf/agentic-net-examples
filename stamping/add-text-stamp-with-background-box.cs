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
        const string stampText  = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp(stampText);

            // Place the stamp behind the page content (background box)
            textStamp.Background = true;

            // Center the stamp on each page
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Optional: make the stamp semi‑transparent
            textStamp.Opacity = 0.6f;

            // Configure the visual appearance of the text
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.FontSize = 24;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.White;          // Text color
            textStamp.TextState.BackgroundColor = Aspose.Pdf.Color.Black;          // Box color

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with text stamp: {outputPath}");
    }
}