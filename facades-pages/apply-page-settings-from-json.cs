using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfPageAutomation
{
    // Represents the JSON configuration for page adjustments.
    public class PageSettingsConfig
    {
        // Rotation in degrees (0, 90, 180, 270). Nullable to allow omission.
        public int? Rotation { get; set; }

        // Zoom factor where 1.0 = 100%.
        public float? Zoom { get; set; }

        // Horizontal alignment: "Left", "Center", "Right".
        public string HorizontalAlignment { get; set; }

        // Vertical alignment: "Top", "Center", "Bottom".
        public string VerticalAlignment { get; set; }

        // Specific page numbers to which the settings should be applied (1‑based indexing).
        public int[] ProcessPages { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string configPath   = "settings.json";   // JSON file with the desired settings
            const string inputPdfPath = "input.pdf";       // Source PDF
            const string outputPdfPath = "output.pdf";     // Result PDF

            // Validate existence of required files.
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Deserialize JSON configuration.
            PageSettingsConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<PageSettingsConfig>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (config == null)
                {
                    Console.Error.WriteLine("Failed to parse configuration.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Open the PDF document and apply the settings via PdfPageEditor.
            try
            {
                using (Document doc = new Document(inputPdfPath))
                using (PdfPageEditor editor = new PdfPageEditor(doc))
                {
                    // Apply rotation if specified.
                    if (config.Rotation.HasValue)
                    {
                        // Valid values are 0, 90, 180, 270.
                        editor.Rotation = config.Rotation.Value;
                    }

                    // Apply zoom if specified.
                    if (config.Zoom.HasValue)
                    {
                        editor.Zoom = config.Zoom.Value;
                    }

                    // Apply horizontal alignment if specified.
                    if (!string.IsNullOrWhiteSpace(config.HorizontalAlignment))
                    {
                        // Parse string to the Aspose.Pdf.HorizontalAlignment enum.
                        editor.HorizontalAlignment = (Aspose.Pdf.HorizontalAlignment)Enum.Parse(
                            typeof(Aspose.Pdf.HorizontalAlignment),
                            config.HorizontalAlignment,
                            ignoreCase: true);
                    }

                    // Apply vertical alignment if specified.
                    if (!string.IsNullOrWhiteSpace(config.VerticalAlignment))
                    {
                        // Parse string to the Aspose.Pdf.VerticalAlignment enum.
                        editor.VerticalAlignmentType = (Aspose.Pdf.VerticalAlignment)Enum.Parse(
                            typeof(Aspose.Pdf.VerticalAlignment),
                            config.VerticalAlignment,
                            ignoreCase: true);
                    }

                    // Restrict processing to specific pages if the array is provided.
                    if (config.ProcessPages != null && config.ProcessPages.Length > 0)
                    {
                        editor.ProcessPages = config.ProcessPages;
                    }

                    // Commit the changes to the document.
                    editor.ApplyChanges();

                    // Save the modified PDF.
                    doc.Save(outputPdfPath);
                }

                Console.WriteLine($"PDF processed successfully. Output saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            }
        }
    }
}