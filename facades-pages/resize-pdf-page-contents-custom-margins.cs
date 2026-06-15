using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfFileEditor instance
            PdfFileEditor editor = new PdfFileEditor();

            // Define custom left and right margins (e.g., 10% left, 15% right)
            // Other margins and content size are left null for automatic calculation
            Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeParameters parameters =
                new Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeParameters(
                    Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                    null,                                                               // contents width (auto)
                    Aspose.Pdf.Facades.PdfFileEditor.ContentsResizeValue.Percents(15), // right margin
                    null,                                                               // top margin (auto)
                    null,                                                               // contents height (auto)
                    null);                                                              // bottom margin (auto)

            // Apply the resize to pages 2 and 4 (page numbers are 1‑based)
            editor.ResizeContents(doc, new int[] { 2, 4 }, parameters);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}