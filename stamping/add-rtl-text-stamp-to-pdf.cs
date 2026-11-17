using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_rtl.pdf";
        // Example Hebrew text; replace with Arabic as needed
        const string rtlText    = "שלום עולם";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a TextStamp that supports right‑to‑left languages
                TextStamp stamp = new TextStamp(rtlText)
                {
                    // Position the stamp near the bottom‑right corner
                    BottomMargin = 20,
                    RightMargin  = 20,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    // Render on top of existing page content
                    Background = false
                };

                // Use a Unicode font that contains Arabic/Hebrew glyphs.
                // Aspose.Pdf will render RTL text automatically when a suitable font is used.
                stamp.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
                stamp.TextState.FontSize = 14;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Apply the stamp to each page (or target specific pages as needed)
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(stamp);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"RTL text stamp added. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
