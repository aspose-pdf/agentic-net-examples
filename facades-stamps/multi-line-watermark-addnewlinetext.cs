using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Common watermark appearance
        const int fontSize = 48;
        Color watermarkColor = Color.FromRgb(0.8, 0.8, 0.8); // light gray

        // Define the three lines with their styles and positions
        var lines = new[]
        {
            new { Text = "CONFIDENTIAL",          FontStyle = FontStyles.Bold,   X = 100.0, Y = 500.0 },
            new { Text = "DO NOT DISTRIBUTE",    FontStyle = FontStyles.Italic, X = 100.0, Y = 450.0 },
            new { Text = "FOR INTERNAL USE ONLY", FontStyle = FontStyles.Regular, X = 100.0, Y = 400.0 }
        };

        // Apply the watermark to every page (or change to a specific page if required)
        foreach (Page page in pdfDoc.Pages)
        {
            foreach (var line in lines)
            {
                // Create a TextFragment for the current line
                TextFragment tf = new TextFragment(line.Text)
                {
                    Position = new Position(line.X, line.Y),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom
                };

                // Set font, size and color for this line
                tf.TextState.Font = FontRepository.FindFont("Arial", line.FontStyle);
                tf.TextState.FontSize = fontSize;
                tf.TextState.ForegroundColor = watermarkColor;

                // Add the fragment to the page
                page.Paragraphs.Add(tf);
            }
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Watermark applied successfully to '{outputPath}'.");
    }
}
