using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF (required for lifecycle compliance; not used for measurement)
        const string inputPdfPath = "input.pdf";
        // Text to measure
        const string text = "Sample text to measure";
        // Desired font name and size
        const string fontName = "Arial";
        const float fontSize = 12f;

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Find the font object
            Font font = FontRepository.FindFont(fontName);
            if (font == null)
            {
                Console.Error.WriteLine($"Font not found: {fontName}");
                return;
            }

            // Measure the string width using the Font.MeasureString method
            double width = font.MeasureString(text, fontSize);

            // Output the measured width
            Console.WriteLine($"Width of \"{text}\" with font {fontName} at size {fontSize}pt: {width} points");
        }
    }
}