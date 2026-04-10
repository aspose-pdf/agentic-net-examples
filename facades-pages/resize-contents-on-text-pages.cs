using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect page numbers (1‑based) that contain any text
        List<int> pagesWithText = new List<int>();

        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);
                if (!string.IsNullOrEmpty(absorber.Text))
                {
                    pagesWithText.Add(i);
                }
            }
        }

        // If no pages contain text, nothing to resize
        if (pagesWithText.Count == 0)
        {
            Console.WriteLine("No pages with text found. No resizing performed.");
            return;
        }

        // Define resize parameters (10% margins on each side)
        PdfFileEditor.ContentsResizeParameters parameters =
            new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                null,                                         // auto width
                PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
                PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
                null,                                         // auto height
                PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
            );

        // Perform resizing only on the selected pages
        PdfFileEditor fileEditor = new PdfFileEditor();
        fileEditor.ResizeContents(
            inputPath,
            outputPath,
            pagesWithText.ToArray(),
            parameters
        );

        Console.WriteLine($"Resizing completed. Output saved to '{outputPath}'.");
    }
}