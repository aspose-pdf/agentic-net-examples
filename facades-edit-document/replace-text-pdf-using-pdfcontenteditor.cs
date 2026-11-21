using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF – this keeps the editor available for any other
            // facade‑level operations you might need later.
            editor.BindPdf(inputPath);

            // Add text using the high‑level Document API (PdfContentEditor does not expose AddText).
            Document pdf = new Document(inputPath);
            TextFragment tf = new TextFragment("Hello, Aspose!");
            // Position specifies the lower‑left corner of the text.
            tf.Position = new Position(100, 500);
            // Optional styling (font, size, color, etc.)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Color.Black;

            // Add the fragment to page 1 (pages are 1‑based).
            pdf.Pages[1].Paragraphs.Add(tf);

            // Save the modified PDF.
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
