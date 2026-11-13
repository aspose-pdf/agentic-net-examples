using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, Font, TextState

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";
        const string prefix = "PRJ-"; // alphanumeric prefix for tracking
        const int digits = 4; // number of digits for zero‑padding (e.g. 0001)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Prepare the font that will be used for the numbering
                Font font = FontRepository.FindFont("Arial");

                // Iterate over all pages and add a TextStamp with the required format
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    // Build the numbering string, e.g. "PRJ-0001"
                    string numbering = $"{prefix}{pageNumber.ToString(new string('0', digits))}";

                    // Create the stamp – TextState is read‑only, so we configure it after construction
                    TextStamp stamp = new TextStamp(numbering)
                    {
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        XIndent = 20,
                        YIndent = 20
                    };

                    // Configure the TextState (font, size, colour)
                    stamp.TextState.Font = font;
                    stamp.TextState.FontSize = 12;
                    stamp.TextState.ForegroundColor = Color.DarkBlue;

                    // Apply the stamp to the current page
                    doc.Pages[pageNumber].AddStamp(stamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Bates numbering applied successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
