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

        // Load the PDF document (lifecycle handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Create the facade for page editing
            PdfFileEditor editor = new PdfFileEditor();

            // Define 10% margins on all sides
            PdfFileEditor.ContentsResizeParameters parameters =
                PdfFileEditor.ContentsResizeParameters.MarginsPercent(10, 10, 10, 10);

            // Resize contents of all pages (null pages array means all pages)
            editor.ResizeContents(doc, parameters);

            // Save the modified document (standard PDF save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}