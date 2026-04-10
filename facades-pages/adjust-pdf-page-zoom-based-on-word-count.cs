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
        const string outputPath = "output_zoomed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count;
            // Store calculated zoom for each page (1‑based indexing)
            float[] zoomPerPage = new float[pageCount + 1];

            // Determine word count per page and decide zoom level
            for (int i = 1; i <= pageCount; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages[i].Accept(absorber);
                string pageText = absorber.Text ?? string.Empty;
                int wordCount = CountWords(pageText);

                // Fewer words → higher zoom for readability
                if (wordCount < 100)
                    zoomPerPage[i] = 1.5f;   // 150%
                else if (wordCount < 300)
                    zoomPerPage[i] = 1.2f;   // 120%
                else
                    zoomPerPage[i] = 1.0f;   // 100%
            }

            // Use PdfPageEditor to apply per‑page zoom
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc); // Initialize facade with the loaded document

                for (int i = 1; i <= pageCount; i++)
                {
                    editor.ProcessPages = new int[] { i }; // Target single page
                    editor.Zoom = zoomPerPage[i];          // Set calculated zoom
                    editor.ApplyChanges();                 // Apply to the page
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }

    // Simple word counter
    static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        // Split on any whitespace characters
        string[] words = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}