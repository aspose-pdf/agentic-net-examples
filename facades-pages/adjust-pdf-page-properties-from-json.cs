using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for VerticalAlignmentType

namespace PdfPageAdjustment
{
    // Represents the JSON configuration for page adjustments.
    public class PageAdjustmentConfig
    {
        public int? Rotation { get; set; }                     // 0, 90, 180, 270
        public double? Zoom { get; set; }                      // 1.0 = 100%
        public string? HorizontalAlignment { get; set; }        // "Left", "Center", "Right"
        public string? VerticalAlignment { get; set; }          // "Bottom", "Middle", "Top"
        public string? PageSize { get; set; }                   // "A4", "Letter", etc.
        public int[]? ProcessPages { get; set; }                // pages to edit (1‑based)
        public int? DisplayDuration { get; set; }              // seconds
        public int? TransitionDuration { get; set; }           // seconds
        public int? TransitionType { get; set; }               // use PdfPageEditor constants (e.g., PdfPageEditor.BLINDH)
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Paths – adjust as needed or obtain from command‑line arguments.
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output.pdf";
            const string configJsonPath = "pageConfig.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(configJsonPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configJsonPath}");
                return;
            }

            // Load and deserialize the JSON configuration.
            PageAdjustmentConfig? config = null;
            try
            {
                string json = File.ReadAllText(configJsonPath);
                config = JsonSerializer.Deserialize<PageAdjustmentConfig>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            if (config == null)
            {
                Console.Error.WriteLine("Configuration could not be deserialized (null result)." );
                return;
            }

            // Apply the configuration using PdfPageEditor (Facade API).
            try
            {
                using (PdfPageEditor editor = new PdfPageEditor())
                {
                    // Bind the source PDF.
                    editor.BindPdf(inputPdfPath);

                    // Set rotation if specified.
                    if (config.Rotation.HasValue)
                        editor.Rotation = config.Rotation.Value;

                    // Set zoom factor if specified (PdfPageEditor.Zoom expects a float representing percentage).
                    if (config.Zoom.HasValue)
                        editor.Zoom = (float)config.Zoom.Value;

                    // Set horizontal alignment if specified.
                    if (!string.IsNullOrWhiteSpace(config.HorizontalAlignment))
                        editor.HorizontalAlignment = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), config.HorizontalAlignment, true);

                    // Set vertical alignment if specified – use the new VerticalAlignmentType enum.
                    if (!string.IsNullOrWhiteSpace(config.VerticalAlignment))
                        editor.VerticalAlignment = (VerticalAlignmentType)Enum.Parse(typeof(VerticalAlignmentType), config.VerticalAlignment, true);

                    // Set page size if specified.
                    if (!string.IsNullOrWhiteSpace(config.PageSize))
                        editor.PageSize = (PageSize)Enum.Parse(typeof(PageSize), config.PageSize, true);

                    // Set which pages to process if specified.
                    if (config.ProcessPages != null && config.ProcessPages.Length > 0)
                        editor.ProcessPages = config.ProcessPages;

                    // Set display duration if specified.
                    if (config.DisplayDuration.HasValue)
                        editor.DisplayDuration = config.DisplayDuration.Value;

                    // Set transition duration if specified.
                    if (config.TransitionDuration.HasValue)
                        editor.TransitionDuration = config.TransitionDuration.Value;

                    // Set transition type if specified (uses the integer constants defined in PdfPageEditor).
                    if (config.TransitionType.HasValue)
                        editor.TransitionType = config.TransitionType.Value;

                    // Apply all changes to the document.
                    editor.ApplyChanges();

                    // Save the modified PDF.
                    editor.Save(outputPdfPath);
                }

                Console.WriteLine($"Page adjustments applied successfully. Output saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during PDF processing: {ex.Message}");
            }
        }
    }
}
