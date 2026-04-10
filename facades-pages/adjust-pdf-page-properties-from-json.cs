using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfPageAdjustment
{
    // Represents the JSON configuration for page adjustments.
    public class PageAdjustmentConfig
    {
        // 1‑based page numbers to which the settings will be applied.
        public int[] Pages { get; set; }

        // Rotation in degrees (0, 90, 180, 270). Optional.
        public int? Rotation { get; set; }

        // Zoom percentage (e.g., 150 for 150%). Optional.
        public int? Zoom { get; set; }

        // Desired page width in points. Optional.
        public float? Width { get; set; }

        // Desired page height in points. Optional.
        public float? Height { get; set; }

        // Transition effect duration in seconds. Optional.
        public int? TransitionDuration { get; set; }

        // Transition type constant from Aspose.Pdf.Facades.PdfPageEditor.
        // Example: Aspose.Pdf.Facades.PdfPageEditor.BLINDH
        public int? TransitionType { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string configPath   = "config.json";          // JSON file with adjustment settings
            const string inputFolder  = "InputPdfs";           // Folder containing source PDFs
            const string outputFolder = "AdjustedPdfs";        // Folder for processed PDFs

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Deserialize the JSON configuration.
            PageAdjustmentConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<PageAdjustmentConfig>(json);
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

            // Ensure output directory exists.
            Directory.CreateDirectory(outputFolder);

            // Process each PDF file in the input folder.
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                string fileName   = Path.GetFileName(pdfPath);
                string outputPath = Path.Combine(outputFolder, fileName);

                try
                {
                    // Load the PDF document.
                    using (Document doc = new Document(pdfPath))
                    {
                        // Initialize the page editor facade.
                        using (PdfPageEditor editor = new PdfPageEditor(doc))
                        {
                            // Apply page selection if specified.
                            if (config.Pages != null && config.Pages.Length > 0)
                                editor.ProcessPages = config.Pages; // int[] required

                            // Apply rotation.
                            if (config.Rotation.HasValue)
                                editor.Rotation = config.Rotation.Value; // int degrees

                            // Apply zoom (percentage). Property expects an integer value.
                            if (config.Zoom.HasValue)
                                editor.Zoom = config.Zoom.Value; // int (e.g., 150 for 150%)

                            // Apply custom page size if both dimensions are provided.
                            if (config.Width.HasValue && config.Height.HasValue)
                            {
                                // PageSize constructor expects float values.
                                Aspose.Pdf.PageSize size = new Aspose.Pdf.PageSize(
                                    config.Width.Value,
                                    config.Height.Value);
                                editor.PageSize = size;
                            }

                            // Apply transition effect duration.
                            if (config.TransitionDuration.HasValue)
                                editor.TransitionDuration = config.TransitionDuration.Value; // int seconds

                            // Apply transition type constant.
                            if (config.TransitionType.HasValue)
                                editor.TransitionType = config.TransitionType.Value;

                            // Commit the changes to the document.
                            editor.ApplyChanges();

                            // Save the modified PDF.
                            editor.Save(outputPath);
                        }
                    }

                    Console.WriteLine($"Processed: {fileName} → {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
                }
            }
        }
    }
}