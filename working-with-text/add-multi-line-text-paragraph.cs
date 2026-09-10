using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new TextParagraph instance
            TextParagraph paragraph = new TextParagraph();

            // Define the rectangle where the paragraph will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            paragraph.Rectangle = new Aspose.Pdf.Rectangle(100, 600, 200, 700);

            // Set word‑wrapping mode (optional, but demonstrates property usage)
            paragraph.FormattingOptions.WrapMode = TextFormattingOptions.WordWrapMode.ByWords;

            // Append multiple lines of text
            paragraph.AppendLine("The quick brown fox jumps over the lazy dog.");
            paragraph.AppendLine("Line two of the paragraph.");
            paragraph.AppendLine("Line three of the paragraph.");

            // Use TextBuilder to add the paragraph to the page
            TextBuilder textBuilder = new TextBuilder(page);
            textBuilder.AppendParagraph(paragraph);

            // Save the modified PDF (saving without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑line paragraph added and saved to '{outputPath}'.");
    }
}