using System;
using System.IO;
using Aspose.Pdf.Facades;
// System.Drawing is required for the Rectangle type expected by PdfContentEditor.CreateFreeText
// It is fully qualified below, so an explicit using directive is optional.

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

        // Load, edit, and save PDF using Aspose.Pdf.Facades
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF file
            editor.BindPdf(inputPath);

            // Example edit: replace all occurrences of "Hello" with "Hi"
            editor.ReplaceText("Hello", "Hi");

            // Example edit: add a free‑text annotation (e.g., a watermark) on page 1
            // PdfContentEditor.CreateFreeText expects a System.Drawing.Rectangle, not Aspose.Pdf.Rectangle.
            // Use a fully‑qualified System.Drawing.Rectangle to avoid ambiguity.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 300, 50); // x, y, width, height
            editor.CreateFreeText(rect, "CONFIDENTIAL", 1);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
