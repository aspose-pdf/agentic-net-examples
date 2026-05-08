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

        // Create resize parameters with 10% margins on all sides.
        PdfFileEditor.ContentsResizeParameters parameters =
            PdfFileEditor.ContentsResizeParameters.MarginsPercent(10, 10, 10, 10);

        // Initialize the facade.
        PdfFileEditor editor = new PdfFileEditor();

        // Load the source PDF, apply resizing, and save the result.
        using (Document doc = new Document(inputPath))
        {
            // Resize contents of all pages using the specified parameters.
            editor.ResizeContents(doc, parameters);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}