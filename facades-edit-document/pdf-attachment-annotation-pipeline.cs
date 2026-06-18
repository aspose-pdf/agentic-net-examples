using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfProcessingPipeline
{
    // Configuration model for the whole pipeline
    public class PipelineConfig
    {
        public string InputPdf { get; set; } = string.Empty;
        public string OutputPdf { get; set; } = string.Empty;
        public List<AttachmentConfig> Attachments { get; set; } = new();
        public List<int> ViewerPreferences { get; set; } = new();
        public List<AnnotationConfig> Annotations { get; set; } = new();
    }

    // Model for a file attachment (embedded in the PDF, no visual annotation)
    public class AttachmentConfig
    {
        public string FilePath { get; set; } = string.Empty;   // Path to the file to embed
        public string Description { get; set; } = string.Empty; // Description shown in attachment list
    }

    // Model for a visual annotation to be added
    public class AnnotationConfig
    {
        public string Type { get; set; } = string.Empty; // Currently supports "FileAttachment"
        public int Page { get; set; } = 1;               // 1‑based page number
        public double Left { get; set; }                 // Lower‑left X coordinate
        public double Bottom { get; set; }               // Lower‑left Y coordinate
        public double Width { get; set; }                // Width of the annotation rectangle
        public double Height { get; set; }               // Height of the annotation rectangle
        public string Contents { get; set; } = string.Empty; // Text shown when annotation is opened
        public string FilePath { get; set; } = string.Empty; // File to attach to the annotation
        public string IconName { get; set; } = "Graph";       // Icon name (Graph, PushPin, Paperclip, Tag)
        public double Opacity { get; set; } = 1.0;            // Opacity from 0 (transparent) to 1 (opaque)
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect a single argument: path to the JSON configuration file
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: PdfProcessingPipeline <config.json>");
                return;
            }

            string configPath = args[0];
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Deserialize configuration
            PipelineConfig config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<PipelineConfig>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new InvalidOperationException("Failed to parse configuration.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error reading configuration: {ex.Message}");
                return;
            }

            // Validate essential paths
            if (!File.Exists(config.InputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {config.InputPdf}");
                return;
            }

            // Ensure output directory exists
            try
            {
                string outputDir = Path.GetDirectoryName(config.OutputPdf) ?? string.Empty;
                if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to prepare output directory: {ex.Message}");
                return;
            }

            // Begin processing with PdfContentEditor
            using (Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(config.InputPdf);

                // 1. Add document attachments (no visual annotation)
                foreach (AttachmentConfig att in config.Attachments)
                {
                    if (!File.Exists(att.FilePath))
                    {
                        Console.Error.WriteLine($"Attachment file not found: {att.FilePath}");
                        continue; // skip missing files
                    }

                    // Add attachment with description
                    editor.AddDocumentAttachment(att.FilePath, att.Description);
                }

                // 2. Apply viewer preferences
                foreach (int pref in config.ViewerPreferences)
                {
                    // ViewerPreference values are defined in Aspose.Pdf.ViewerPreference static class
                    editor.ChangeViewerPreference(pref);
                }

                // 3. Insert visual annotations
                foreach (AnnotationConfig ann in config.Annotations)
                {
                    // Currently only FileAttachment annotation is demonstrated
                    if (string.Equals(ann.Type, "FileAttachment", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!File.Exists(ann.FilePath))
                        {
                            Console.Error.WriteLine($"Annotation attachment file not found: {ann.FilePath}");
                            continue;
                        }

                        // System.Drawing.Rectangle is required by the API – fully qualified to avoid ambiguity
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                            (int)Math.Round(ann.Left),
                            (int)Math.Round(ann.Bottom),
                            (int)Math.Round(ann.Width),
                            (int)Math.Round(ann.Height));

                        // Use the overload that allows specifying opacity
                        editor.CreateFileAttachment(
                            rect,
                            ann.Contents,
                            ann.FilePath,
                            ann.Page,
                            ann.IconName,
                            ann.Opacity);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Unsupported annotation type: {ann.Type}");
                    }
                }

                // Save the modified PDF
                editor.Save(config.OutputPdf);
            }

            Console.WriteLine($"Processing completed. Output saved to '{config.OutputPdf}'.");
        }
    }
}
