using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText = "שלום עולם"; // Hebrew example (Arabic works similarly)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Create a text stamp. Use a Unicode font that contains Arabic/Hebrew glyphs.
                TextStamp textStamp = new TextStamp(stampText);
                textStamp.TextState.Font = FontRepository.FindFont("Arial Unicode MS"); // font supporting RTL scripts
                textStamp.TextState.FontSize = 24;
                textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;

                // Position the stamp – centered at the top of each page.
                textStamp.HorizontalAlignment = HorizontalAlignment.Center;
                textStamp.VerticalAlignment = VerticalAlignment.Top;
                // Use YIndent (or XIndent) instead of the non‑existent Margin property.
                textStamp.YIndent = 10; // distance from the top edge in points

                // Apply the stamp to every page.
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(textStamp);
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with RTL text stamp to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
