using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;      // PdfPageEditor facade
using Aspose.Pdf.Text;         // TextAbsorber for word counting

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor works on the same Document instance
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Extract text from the current page
                    TextAbsorber absorber = new TextAbsorber();
                    doc.Pages[pageNum].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // Count words on the page
                    int wordCount = pageText.Split(
                        new char[] { ' ', '\t', '\r', '\n' },
                        StringSplitOptions.RemoveEmptyEntries).Length;

                    // Determine zoom factor:
                    //   Fewer words  -> larger zoom (more readable)
                    //   Many words   -> smaller zoom (fit more content)
                    //   Default      -> 1.0 (100%)
                    float zoom = 1.0f;
                    if (wordCount < 100)          // very short page
                        zoom = 1.5f;               // 150%
                    else if (wordCount > 500)     // dense page
                        zoom = 0.8f;               // 80%
                    // else keep default 1.0

                    // Apply zoom only to the current page
                    editor.ProcessPages = new int[] { pageNum };
                    editor.Zoom = zoom;
                    editor.ApplyChanges();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }
}