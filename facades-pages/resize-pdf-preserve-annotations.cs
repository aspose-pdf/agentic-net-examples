using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define resize parameters – 10% margins on each side
            PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10.0), // left margin
                null,                                            // auto‑calculate new width
                PdfFileEditor.ContentsResizeValue.Percents(10.0), // right margin
                PdfFileEditor.ContentsResizeValue.Percents(10.0), // top margin
                null,                                            // auto‑calculate new height
                PdfFileEditor.ContentsResizeValue.Percents(10.0)  // bottom margin
            );

            // Resize all pages – annotations and interactive elements are kept
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.ResizeContents(doc, parameters);

            // Save the resized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}