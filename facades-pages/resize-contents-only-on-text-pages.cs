using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            List<int> pagesWithText = new List<int>();

            // Iterate through pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);

                // If the page contains any text, record its index
                if (!string.IsNullOrEmpty(absorber.Text))
                {
                    pagesWithText.Add(i);
                }
            }

            // If no pages contain text, simply save the original document
            if (pagesWithText.Count == 0)
            {
                doc.Save(outputPath);
                Console.WriteLine("No text found on any page; original PDF saved unchanged.");
                return;
            }

            // Define resize parameters (10% margins on each side)
            PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
                PdfFileEditor.ContentsResizeValue.Percents(10), // left margin
                null,                                          // auto‑calculate content width
                PdfFileEditor.ContentsResizeValue.Percents(10), // right margin
                PdfFileEditor.ContentsResizeValue.Percents(10), // top margin
                null,                                          // auto‑calculate content height
                PdfFileEditor.ContentsResizeValue.Percents(10)  // bottom margin
            );

            // Resize only the pages that contain text
            PdfFileEditor fileEditor = new PdfFileEditor();
            fileEditor.ResizeContents(doc, pagesWithText.ToArray(), parameters);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}