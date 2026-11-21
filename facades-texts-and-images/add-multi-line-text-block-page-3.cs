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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level Document API
        Document pdf = new Document(inputPath);

        // Ensure the document has at least three pages
        if (pdf.Pages.Count < 3)
        {
            Console.Error.WriteLine("The PDF does not contain a third page.");
            return;
        }

        // Get page 3 and its size
        Page page3 = pdf.Pages[3];
        double pageHeight = page3.PageInfo.Height; // PageInfo.Height returns double
        // Define the left‑margin X coordinate (0) and a Y coordinate that starts near the top
        // Position uses the lower‑left corner, so we subtract a small offset from the top.
        double leftX = 0.0;
        double topY = pageHeight - 12.0; // 12 points down from the top edge

        // Multi‑line text (CRLF works for line breaks)
        string multiLineText = "First line\r\nSecond line\r\nThird line";

        // Create a TextFragment and apply the desired styling via the existing TextState instance
        TextFragment tf = new TextFragment(multiLineText);
        tf.Position = new Position(leftX, topY);
        tf.TextState.Font = FontRepository.FindFont("Arial");
        tf.TextState.FontSize = 12;
        tf.TextState.LineSpacing = 5; // custom line spacing in points

        // Add the fragment to page 3
        page3.Paragraphs.Add(tf);

        // Save the modified PDF
        pdf.Save(outputPath);

        Console.WriteLine($"Multi‑line text added to page 3 and saved as '{outputPath}'.");
    }
}
