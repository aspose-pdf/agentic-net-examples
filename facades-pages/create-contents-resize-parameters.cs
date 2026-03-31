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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create resize parameters with 5 points margins on all sides
            PdfFileEditor.ContentsResizeParameters resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(5, 5, 5, 5);

            // Prepare an array with all page numbers (1‑based indexing)
            int[] pageNumbers = new int[doc.Pages.Count];
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                pageNumbers[i - 1] = i;
            }

            // Apply the resize parameters to the selected pages
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(doc, pageNumbers, resizeParams);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Resized PDF saved to '" + outputPath + "'.");
    }
}
