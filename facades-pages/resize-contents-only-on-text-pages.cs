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
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine which pages contain text
        List<int> pagesWithText = new List<int>();
        using (Document doc = new Document(inputPath))
        {
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);
                if (!string.IsNullOrEmpty(absorber.Text))
                {
                    pagesWithText.Add(i);
                }
            }
        }

        // If no pages contain text, simply copy the source to destination
        if (pagesWithText.Count == 0)
        {
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("No text found on any page. Original file copied.");
            return;
        }

        // Set resize parameters (10% margins on each side)
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
            null,                                          // auto width
            PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
            null,                                          // auto height
            PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
        );

        // Resize only the pages that contain text
        PdfFileEditor fileEditor = new PdfFileEditor();
        fileEditor.ResizeContents(
            inputPath,
            outputPath,
            pagesWithText.ToArray(),
            parameters
        );

        Console.WriteLine($"Resized pages saved to '{outputPath}'.");
    }
}