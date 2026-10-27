using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Custom Bates numbering configuration
        const string prefix = "DOC";
        const string suffix = "-2026";
        const int startNumber = 1;          // first number to use
        const int numberOfDigits = 4;       // zero‑pad to this width

        // Load the PDF document using the core API
        Document doc = new Document(inputPath);

        // Apply a TextStamp to each page
        for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
        {
            int currentNumber = startNumber + pageIndex - 1;
            string numberString = currentNumber.ToString().PadLeft(numberOfDigits, '0');
            string stampText = $"{prefix}{numberString}{suffix}";

            // Create the stamp and configure its appearance
            TextStamp stamp = new TextStamp(stampText)
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent = 10,   // distance from the right edge
                YIndent = 10    // distance from the bottom edge
            };

            // TextState is read‑only; modify the existing instance instead of assigning a new one
            stamp.TextState.Font = FontRepository.FindFont("Arial");
            stamp.TextState.FontSize = 12;
            stamp.TextState.FontStyle = FontStyles.Bold;
            stamp.TextState.ForegroundColor = Color.Black;

            doc.Pages[pageIndex].AddStamp(stamp);
        }

        // Save the modified PDF
        doc.Save(outputPath);
        Console.WriteLine($"Bates numbering applied. Output saved to '{outputPath}'.");
    }
}
