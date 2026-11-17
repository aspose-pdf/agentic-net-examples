using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextState and TextRenderingMode

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText  = "OUTLINE TEXT";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Position the stamp (example: centered on the page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Optional: make the background transparent
                Background = false
            };

            // Configure the appearance of the text via the TextState object
            textStamp.TextState.Font = FontRepository.FindFont("Arial");
            textStamp.TextState.FontSize = 48;
            textStamp.TextState.ForegroundColor = Color.Black; // color of the outline
            // Use the integer value for the Stroke rendering mode to avoid enum name mismatches
            textStamp.TextState.RenderingMode = (TextRenderingMode)1; // 1 == Stroke

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(textStamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Text stamp with stroke rendering saved to '{outputPath}'.");
    }
}
