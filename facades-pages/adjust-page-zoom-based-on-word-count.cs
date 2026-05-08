using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "zoom_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare the page editor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the loaded document to the editor
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
                        new char[] { ' ', '\n', '\r', '\t' },
                        StringSplitOptions.RemoveEmptyEntries).Length;

                    // Determine zoom factor: fewer words => higher zoom
                    float zoomFactor;
                    if (wordCount < 200)
                        zoomFactor = 1.5f;   // 150%
                    else if (wordCount < 400)
                        zoomFactor = 1.2f;   // 120%
                    else
                        zoomFactor = 1.0f;   // 100%

                    // Apply zoom to the current page
                    editor.ProcessPages = new int[] { pageNum };
                    editor.Zoom = zoomFactor;
                    editor.ApplyChanges();
                }

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }
}