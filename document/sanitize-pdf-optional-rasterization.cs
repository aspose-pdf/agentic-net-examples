using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security.HiddenDataSanitization;
// using System.Drawing; // Uncomment if you need Size for custom rasterization options

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor inside a using block)
            using (Document doc = new Document(inputPath))
            {
                // ----- Hidden data sanitization using built‑in options -----
                // HiddenDataSanitizationOptions defines what hidden data to remove.
                var sanitizationOptions = new HiddenDataSanitizationOptions
                {
                    RemoveJavaScriptsAndActions = true,
                    RemoveSearchIndexAndPrivateInfo = true,
                    RemoveAnnotations = true,
                    FlattenForms = true,
                    FlattenLayers = true
                };
                var sanitizer = new HiddenDataSanitizer(sanitizationOptions);
                sanitizer.Sanitize(doc);

                // ----- Selective page rasterization -----
                // Rasterization is controlled via PdfSaveOptions.RasterizationOptions.
                // Setting RasterizationOptions enables rasterization for all pages.
                // To rasterize only specific pages you would need to split the document or
                // use a custom page range handling (not shown here).
                var saveOptions = new PdfSaveOptions();

                // Example rasterization settings (optional). Comment out if not needed.
                // saveOptions.RasterizationOptions = new RasterizationOptions
                // {
                //     Resolution = 300,
                //     // PageSize = new Size(595, 842) // A4 size in points – requires System.Drawing
                // };

                // Save the sanitized (and optionally rasterized) PDF
                doc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
