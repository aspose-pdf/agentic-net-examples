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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Define resize parameters with 10% margins on all sides
            PdfFileEditor.ContentsResizeParameters parameters =
                PdfFileEditor.ContentsResizeParameters.MarginsPercent(10, 10, 10, 10);

            // Apply the resize to all pages
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(doc, parameters);

            // Save the resized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}