using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for TextStamp, TextState, FontRepository, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_stamped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to every page
            TextStamp textStamp = new TextStamp("CONFIDENTIAL")
            {
                // Position the stamp at the center of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,

                // Make the stamp semi‑transparent
                Opacity = 0.5f,

                // Optional: set background flag if you want the stamp behind page content
                Background = false
            };

            // Configure the appearance of the text via the read‑only TextState object
            textStamp.TextState.FontSize = 48;
            textStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            textStamp.TextState.ForegroundColor = Color.Red;

            // Apply the stamp to all pages by iterating over the Pages collection
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}