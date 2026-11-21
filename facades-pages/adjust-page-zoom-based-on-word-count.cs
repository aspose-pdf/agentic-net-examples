using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // needed for TextFragmentAbsorber

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "zoom_adjusted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document for text extraction
        using (Document doc = new Document(inputPath))
        {
            // PdfPageEditor is used to apply per‑page zoom values
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(inputPath);

                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // ----- Extract text from the current page -----
                    TextFragmentAbsorber absorber = new TextFragmentAbsorber();
                    absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                    doc.Pages[i].Accept(absorber);
                    string pageText = absorber.Text ?? string.Empty;

                    // ----- Determine zoom based on word count -----
                    int wordCount = CountWords(pageText);
                    double zoom = ComputeZoom(wordCount);

                    // ----- Apply the zoom to the current page -----
                    editor.ProcessPages = new int[] { i };   // select the page
                    editor.Zoom = (float)zoom;               // set zoom (0.5‑2.0 range)
                    editor.ApplyChanges();                  // apply to the bound document
                }

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Zoom‑adjusted PDF saved to '{outputPath}'.");
    }

    // Compute zoom factor – fewer words => higher zoom, clamped between 0.5 and 2.0
    private static double ComputeZoom(int wordCount)
    {
        if (wordCount == 0)
            return 2.0; // maximum zoom for empty pages

        double zoom = 2000.0 / wordCount; // arbitrary scaling factor
        if (zoom < 0.5) zoom = 0.5;
        if (zoom > 2.0) zoom = 2.0;
        return zoom;
    }

    // Simple word counter
    private static int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;

        string[] words = text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}
