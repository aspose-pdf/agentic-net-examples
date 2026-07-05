using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;

class Program
{
    // Configuration model matching the appsettings.json structure
    private class AppConfig
    {
        public double ImageScaleFactor { get; set; } = 1.0; // default: no scaling
    }

    static void Main()
    {
        const string configPath   = "appsettings.json";   // path to configuration file
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "output_resized.pdf"; // destination PDF

        // -----------------------------------------------------------------
        // Load scaling factor from JSON configuration
        // -----------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        AppConfig config;
        try
        {
            string json = File.ReadAllText(configPath);
            config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        double scaleFactor = config.ImageScaleFactor;
        if (scaleFactor <= 0)
        {
            Console.Error.WriteLine("ImageScaleFactor must be a positive number.");
            return;
        }

        // -----------------------------------------------------------------
        // Open the PDF, resize each Image paragraph, and save the result
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            using (Document pdfDoc = new Document(inputPdfPath)) // load PDF
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate over all paragraph objects on the page
                    for (int i = 0; i < page.Paragraphs.Count; i++)
                    {
                        // The Paragraphs collection can contain different types; we need Image objects
                        if (page.Paragraphs[i] is Image img)
                        {
                            // Apply the scaling factor defined in the configuration
                            img.ImageScale = scaleFactor;
                        }
                    }
                }

                // Save the modified PDF (using the standard Save method)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved with resized images to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}