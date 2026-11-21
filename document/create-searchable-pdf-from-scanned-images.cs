using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "scanned.pdf";
        const string outputPath = "searchable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the scanned PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf.
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // TODO: Replace this stub with real OCR extraction logic.
                    string ocrText = GetOcrTextForPage(page, i);
                    if (string.IsNullOrEmpty(ocrText))
                        continue;

                    // Create a text fragment that will be invisible.
                    TextFragment tf = new TextFragment(ocrText);
                    tf.TextState.Font = FontRepository.FindFont("Arial");
                    tf.TextState.FontSize = 0.1F; // float literal required
                    // Fully transparent text – use an ARGB color with alpha = 0.
                    tf.TextState.ForegroundColor = Color.FromArgb(0, 0, 0, 0);
                    tf.Position = new Position(0, 0); // start at page origin

                    // Add the invisible text to the page.
                    page.Paragraphs.Add(tf);
                }

                // Save the resulting searchable PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Searchable PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Placeholder OCR method – integrate Aspose.OCR or another OCR engine here.
    static string GetOcrTextForPage(Page page, int pageNumber)
    {
        // Return extracted text for the given page.
        // For demonstration purposes this returns an empty string.
        return string.Empty;
    }
}