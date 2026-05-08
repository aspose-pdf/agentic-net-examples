using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "appsettings.json";

        // ---------------------------------------------------------------------
        // 1. Read scaling factor from appsettings.json (default = 1.0 – no scaling)
        // ---------------------------------------------------------------------
        double scaleFactor = 1.0;
        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            using JsonDocument doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("ImageScaleFactor", out JsonElement factorElem) &&
                factorElem.TryGetDouble(out double factor))
            {
                scaleFactor = factor;
            }
        }

        // ---------------------------------------------------------------
        // 2. Ensure the input PDF exists – create a minimal placeholder if it does not.
        // ---------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            CreatePlaceholderPdf(inputPdfPath);
            Console.WriteLine($"Placeholder PDF created at '{inputPdfPath}'.");
        }

        // ---------------------------------------------------------------
        // 3. Load the PDF, resize any images, and save the result.
        // ---------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Iterate over all paragraph elements on the page.
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    // Identify Image objects (Aspose.Pdf.Image).
                    if (page.Paragraphs[i] is Aspose.Pdf.Image img)
                    {
                        // Apply the scaling factor read from configuration.
                        img.ImageScale = scaleFactor;
                    }
                }
            }

            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Images resized with factor {scaleFactor} and saved to '{outputPdfPath}'.");
    }

    /// <summary>
    /// Creates a very small PDF containing a single blank page. This method is used only
    /// when the expected input file does not exist, preventing a FileNotFoundException at runtime.
    /// </summary>
    /// <param name="path">Full path where the placeholder PDF should be saved.</param>
    private static void CreatePlaceholderPdf(string path)
    {
        // Create a new empty document.
        using (Document doc = new Document())
        {
            // Add a single blank page (default size A4).
            doc.Pages.Add();
            doc.Save(path);
        }
    }
}
