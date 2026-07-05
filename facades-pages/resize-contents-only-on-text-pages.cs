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
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collect page numbers that contain any text
        List<int> pagesWithText = new List<int>();
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);
                if (!string.IsNullOrWhiteSpace(absorber.Text))
                {
                    pagesWithText.Add(i);
                }
            }
        }

        if (pagesWithText.Count == 0)
        {
            // No pages with text – simply copy the source file
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("No text found on any page. File copied without resizing.");
            return;
        }

        // Define resize parameters (10% margins on each side)
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
            null,                                          // auto width
            PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
            null,                                          // auto height
            PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
        );

        // Perform resize only on the pages that contain text
        PdfFileEditor fileEditor = new PdfFileEditor();
        bool success = fileEditor.ResizeContents(
            inputPath,
            outputPath,
            pagesWithText.ToArray(),
            parameters
        );

        Console.WriteLine(success
            ? $"Resizing completed. Output saved to '{outputPath}'."
            : "Resizing failed.");
    }
}