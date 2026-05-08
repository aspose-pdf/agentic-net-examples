using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_rtl_stamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Arabic text (right‑to‑left language). Any Hebrew text could be used similarly.
            const string rtlText = "مرحبا بالعالم"; // "Hello World" in Arabic

            // Create a TextStamp with the RTL text
            TextStamp stamp = new TextStamp(rtlText);

            // Configure the visual appearance of the stamp
            // Use a font that supports Arabic/Hebrew characters
            stamp.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
            stamp.TextState.FontSize = 24;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0, 1); // Blue

            // Position the stamp (example: centered on each page)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Center;

            // Optional: make the stamp semi‑transparent and place it on top of content
            stamp.Opacity   = 0.6;
            stamp.Background = false; // false = draw on top

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with RTL text stamp saved to '{outputPath}'.");
    }
}