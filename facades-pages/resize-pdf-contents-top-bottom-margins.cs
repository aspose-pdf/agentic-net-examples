using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ---------------------------------------------------------------------
        // Ensure the input file exists. In the sandbox there is no pre‑existing
        // PDF, so we create a minimal one on‑the‑fly with three pages. This
        // satisfies the "hardcoded‑input‑file‑generate‑inline‑first" rule and
        // prevents the "Invalid page index" exception when we later request
        // pages 1, 2 and 3.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document seed = new Document())
            {
                // Add three pages, each with a simple text fragment so the
                // resize operation has something to work with.
                for (int i = 1; i <= 3; i++)
                {
                    Page page = seed.Pages.Add();
                    page.Paragraphs.Add(new TextFragment($"Sample content for page {i}"));
                }
                seed.Save(inputPath);
            }
        }

        // Page numbers to process (1‑based indexing). Use null to process all pages.
        int[] pages = new int[] { 1, 2, 3 };

        // PdfFileEditor does not implement IDisposable, so we instantiate it
        // directly without a using block.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Build resize parameters:
        // • Top margin = 10 % of the original page height
        // • Bottom margin = 20 % of the original page height
        // • Left, right margins and content size are left to auto‑calculate (null).
        PdfFileEditor.ContentsResizeParameters parameters = new PdfFileEditor.ContentsResizeParameters(
            leftMargin:      null,                                            // auto
            contentsWidth:   null,                                            // auto
            rightMargin:     null,                                            // auto
            topMargin:       PdfFileEditor.ContentsResizeValue.Percents(10), // 10 % top
            contentsHeight:  null,                                            // auto
            bottomMargin:    PdfFileEditor.ContentsResizeValue.Percents(20)  // 20 % bottom
        );

        // Perform the resize operation. This overload writes the result directly
        // to the destination file.
        fileEditor.ResizeContents(inputPath, outputPath, pages, parameters);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
