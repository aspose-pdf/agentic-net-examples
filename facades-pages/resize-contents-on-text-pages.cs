using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
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

        // Collect page numbers that contain any text
        List<int> pagesWithText = new List<int>();
        using (Document doc = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);
                if (!String.IsNullOrEmpty(absorber.Text))
                {
                    pagesWithText.Add(pageIndex);
                }
            }
        }

        if (pagesWithText.Count == 0)
        {
            Console.WriteLine("No pages with text found – nothing to resize.");
            return;
        }

        // Define resize margins (10% on each side)
        PdfFileEditor.ContentsResizeParameters resizeParams = new PdfFileEditor.ContentsResizeParameters(
            PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
            null,                                          // auto width
            PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
            PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
            null,                                          // auto height
            PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
        );

        PdfFileEditor fileEditor = new PdfFileEditor();
        bool result = fileEditor.TryResizeContents(
            inputPath,
            outputPath,
            pagesWithText.ToArray(),
            resizeParams
        );

        Console.WriteLine(result ? "Resize operation completed successfully." : "Resize operation failed.");
    }
}
