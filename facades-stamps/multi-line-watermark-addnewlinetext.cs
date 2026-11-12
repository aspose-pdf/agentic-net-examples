using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Font, FontRepository, FontStyles, TextStamp

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain page size and to apply stamps.
        Document doc = new Document(inputPath);
        Page firstPage = doc.Pages[1]; // 1‑based indexing
        double pageWidth = firstPage.PageInfo.Width;
        double pageHeight = firstPage.PageInfo.Height;

        // Center X is not used directly – we centre via HorizontalAlignment.
        // Y‑coordinates are handled as floats because TextStamp.YIndent expects a float.
        float startY = (float)(pageHeight / 2 + 30); // first line a bit above centre
        float lineSpacing = 20f;
        // FontSize in Aspose.Pdf.Text.TextState is a float, so use float here.
        float fontSize = 36f;

        // Define the three lines together with the font name and the desired style.
        string[] texts = { "CONFIDENTIAL", "Do Not Distribute", "© Company 2026" };
        string[] fonts = { "Arial", "Times New Roman", "Courier New" };
        FontStyles[] styles = { FontStyles.Bold, FontStyles.Italic, FontStyles.Regular };

        for (int i = 0; i < texts.Length; i++)
        {
            // Create a TextStamp for the current line.
            TextStamp stamp = new TextStamp(texts[i]);

            // Assign font, size, style and colour.
            stamp.TextState.Font = FontRepository.FindFont(fonts[i]);
            stamp.TextState.FontSize = fontSize; // now matches the property type (float)
            stamp.TextState.FontStyle = styles[i];
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.5, 0.5, 0.5);

            // Centre the text horizontally and position it vertically.
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            // Explicit cast to float because YIndent is a float property.
            stamp.YIndent = (float)(startY - i * lineSpacing);

            // Optional: make the watermark semi‑transparent.
            stamp.Opacity = 0.5f;

            // Add the stamp to the first page (or loop over all pages if required).
            firstPage.AddStamp(stamp);
        }

        // Save the watermarked PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
