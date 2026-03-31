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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document document = new Document(inputPath))
        {
            // Prepare page numbers (1‑based indexing)
            int pageCount = document.Pages.Count;
            int[] pageNumbers = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
            {
                pageNumbers[i] = i + 1;
            }

            // Define asymmetric margins: left = 50 units, right = 20 units, top = 0, bottom = 0
            PdfFileEditor.ContentsResizeParameters resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(50.0, 20.0, 0.0, 0.0);

            // Apply the resizing to the selected pages
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(document, pageNumbers, resizeParams);

            // Save the modified PDF
            document.Save(outputPath);
            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
    }
}