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
        const double marginPoints = 50.0; // uniform margin on all sides

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare an array with all page numbers (1‑based indexing)
            int pageCount = doc.Pages.Count;
            int[] allPages = new int[pageCount];
            for (int i = 0; i < pageCount; i++)
            {
                allPages[i] = i + 1;
            }

            // Create resize parameters with uniform margins
            PdfFileEditor.ContentsResizeParameters resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(marginPoints, marginPoints, marginPoints, marginPoints);

            // Resize the contents of the selected pages
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(doc, allPages, resizeParams);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}