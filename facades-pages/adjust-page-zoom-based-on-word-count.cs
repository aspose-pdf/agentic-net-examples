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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the page editor facade (belongs to Aspose.Pdf.Facades)
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
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

                    // Determine zoom factor: higher zoom for fewer words
                    float zoom;
                    if (wordCount < 100)          // very short page
                        zoom = 1.5f;               // enlarge for readability
                    else if (wordCount > 500)     // very dense page
                        zoom = 0.8f;               // shrink to fit more content
                    else
                        zoom = 1.0f;               // default zoom

                    // Apply zoom to the current page only
                    editor.ProcessPages = new int[] { pageNum };
                    editor.Zoom = zoom;
                    editor.ApplyChanges();
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}