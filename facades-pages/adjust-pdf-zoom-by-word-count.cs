using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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
            int pageCount = doc.Pages.Count;
            int[] wordCounts = new int[pageCount];

            // Extract word count for each page using TextAbsorber
            for (int i = 1; i <= pageCount; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;

                // Simple word split on whitespace characters
                wordCounts[i - 1] = string.IsNullOrWhiteSpace(pageText)
                    ? 0
                    : pageText.Split(new char[] { ' ', '\n', '\r', '\t' },
                                    StringSplitOptions.RemoveEmptyEntries).Length;
            }

            // Determine zoom range: lower zoom for dense pages, higher zoom for sparse pages
            int maxWords = wordCounts.Max();
            int minWords = wordCounts.Min();

            const float minZoom = 1.0f; // 100%
            const float maxZoom = 1.5f; // 150%

            // Use PdfPageEditor to apply per‑page zoom settings
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the existing Document instance to the editor
                editor.BindPdf(doc);

                for (int i = 1; i <= pageCount; i++)
                {
                    int wc = wordCounts[i - 1];
                    float zoom;

                    // If all pages have the same word count, keep default zoom
                    if (maxWords == minWords)
                    {
                        zoom = minZoom;
                    }
                    else
                    {
                        // Inverse linear mapping: fewer words → higher zoom
                        float ratio = (float)(maxWords - wc) / (maxWords - minWords);
                        zoom = minZoom + (maxZoom - minZoom) * ratio;
                    }

                    // Apply zoom to the current page only
                    editor.ProcessPages = new int[] { i };
                    editor.Zoom = zoom;
                    editor.ApplyChanges();
                }

                // Save the modified PDF to the output path
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }
}