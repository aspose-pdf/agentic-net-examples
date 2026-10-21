using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the font to be used for all watermark lines
            Aspose.Pdf.Text.Font watermarkFont = FontRepository.FindFont("Arial");

            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // First line – largest font size
                TextFragment line1 = new TextFragment("CONFIDENTIAL");
                line1.TextState.Font = watermarkFont;
                line1.TextState.FontSize = 48;
                line1.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                // Center horizontally, 70% up the page height
                line1.Position = new Position(
                    (page.PageInfo.Width - line1.TextState.FontSize * line1.Text.Length) / 2,
                    page.PageInfo.Height * 0.7);
                page.Paragraphs.Add(line1);

                // Second line – medium font size
                TextFragment line2 = new TextFragment("Do Not Distribute");
                line2.TextState.Font = watermarkFont;
                line2.TextState.FontSize = 32;
                line2.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                line2.Position = new Position(
                    (page.PageInfo.Width - line2.TextState.FontSize * line2.Text.Length) / 2,
                    page.PageInfo.Height * 0.6);
                page.Paragraphs.Add(line2);

                // Third line – smallest font size
                TextFragment line3 = new TextFragment("For Internal Use Only");
                line3.TextState.Font = watermarkFont;
                line3.TextState.FontSize = 24;
                line3.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                line3.Position = new Position(
                    (page.PageInfo.Width - line3.TextState.FontSize * line3.Text.Length) / 2,
                    page.PageInfo.Height * 0.5);
                page.Paragraphs.Add(line3);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}