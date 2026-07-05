using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_bold_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp stamp = new TextStamp("Bold Outlined Text")
            {
                // Render as graphic operators (required for rendering mode in older versions)
                Draw = true,

                // Center the stamp on each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Configure the text appearance – use a bold font to simulate an outlined effect
            stamp.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            stamp.TextState.FontSize = 48;
            // Fill color (the interior of the characters)
            stamp.TextState.ForegroundColor = Color.Blue;
            // Note: StrokeColor and RenderingMode are not available in this version of Aspose.Pdf.
            // The bold font provides a thicker, pseudo‑outlined appearance.

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamp added and saved to '{outputPath}'.");
    }
}
