using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing; // used for System.Drawing.Rectangle in annotations
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfPipelineExample
{
    // Configuration classes
    public class PipelineConfig
    {
        // Provide default empty strings to satisfy non‑nullable warnings
        public string InputPath { get; set; } = string.Empty;
        public string OutputPath { get; set; } = string.Empty;
        public List<AttachmentConfig> Attachments { get; set; } = new List<AttachmentConfig>();
        // Viewer preferences are represented as integer flags in Aspose.Pdf.Facades
        public List<int> ViewerPreferences { get; set; } = new List<int>();
        public List<AnnotationConfig> Annotations { get; set; } = new List<AnnotationConfig>();
    }

    public class AttachmentConfig
    {
        public string? FilePath { get; set; }
        public string? Description { get; set; }
    }

    public class AnnotationConfig
    {
        public string? Type { get; set; }               // "FreeText", "WebLink", "FileAttachment"
        public double Llx { get; set; }
        public double Lly { get; set; }
        public double Urx { get; set; }
        public double Ury { get; set; }
        public string? Text { get; set; }               // used for FreeText
        public string? Url { get; set; }                // used for WebLink
        public string? FilePath { get; set; }           // used for FileAttachment
        public string? Description { get; set; }        // used for FileAttachment
    }

    class Program
    {
        static void Main()
        {
            // Example configuration (could be loaded from JSON, XML, etc.)
            PipelineConfig config = new PipelineConfig
            {
                InputPath = "input.pdf",
                OutputPath = "output.pdf",
                Attachments = new List<AttachmentConfig>
                {
                    new AttachmentConfig { FilePath = "attachment1.docx", Description = "First attachment" },
                    new AttachmentConfig { FilePath = "attachment2.xlsx", Description = "Second attachment" }
                },
                ViewerPreferences = new List<int>
                {
                    (int)ViewerPreference.HideMenubar,
                    (int)ViewerPreference.PageModeUseNone
                },
                Annotations = new List<AnnotationConfig>
                {
                    new AnnotationConfig
                    {
                        Type = "FreeText",
                        Llx = 100, Lly = 700, Urx = 300, Ury = 750,
                        Text = "Confidential"
                    },
                    new AnnotationConfig
                    {
                        Type = "WebLink",
                        Llx = 50, Lly = 600, Urx = 200, Ury = 620,
                        Url = "https://www.example.com"
                    },
                    new AnnotationConfig
                    {
                        Type = "FileAttachment",
                        Llx = 400, Lly = 500, Urx = 420, Ury = 520,
                        FilePath = "attachment1.docx",
                        Description = "Attached document"
                    }
                }
            };

            // Validate input file existence
            if (!File.Exists(config.InputPath))
            {
                Console.Error.WriteLine($"Input file not found: {config.InputPath}");
                return;
            }

            // Process pipeline using PdfContentEditor facade
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load source PDF
                editor.BindPdf(config.InputPath);

                // Add document attachments
                foreach (var att in config.Attachments)
                {
                    if (!string.IsNullOrEmpty(att.FilePath) && File.Exists(att.FilePath))
                    {
                        editor.AddDocumentAttachment(att.FilePath, att.Description ?? string.Empty);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Attachment file not found or path is null: {att.FilePath}");
                    }
                }

                // Set viewer preferences (the facade expects integer flags)
                foreach (var pref in config.ViewerPreferences)
                {
                    editor.ChangeViewerPreference(pref);
                }

                // Insert annotations based on type – use System.Drawing.Rectangle as required by the facade methods
                foreach (var ann in config.Annotations)
                {
                    // Convert the double coordinates to a System.Drawing.Rectangle (int values are expected)
                    System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                        (int)ann.Llx,
                        (int)ann.Lly,
                        (int)(ann.Urx - ann.Llx),
                        (int)(ann.Ury - ann.Lly));

                    switch (ann.Type?.Trim().ToLowerInvariant())
                    {
                        case "freetext":
                            editor.CreateFreeText(rect, ann.Text ?? string.Empty, 0);
                            break;

                        case "weblink":
                            editor.CreateWebLink(rect, ann.Url ?? string.Empty, 0);
                            break;

                        case "fileattachment":
                            editor.CreateFileAttachment(
                                rect,
                                ann.Description ?? string.Empty,
                                ann.FilePath ?? string.Empty,
                                0,
                                string.Empty);
                            break;

                        default:
                            Console.Error.WriteLine($"Unsupported annotation type: {ann.Type}");
                            break;
                    }
                }

                // Save the modified PDF
                editor.Save(config.OutputPath);
            }

            Console.WriteLine($"Pipeline completed. Output saved to '{config.OutputPath}'.");
        }
    }
}
