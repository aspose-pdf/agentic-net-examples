using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFacadePipeline
{
    // Base class for all pipeline actions
    abstract class ActionConfig
    {
        // Marked as non‑nullable but initialized with the null‑forgiving operator to silence warnings.
        public string ActionType { get; set; } = null!;
    }

    // Action to add a file attachment to the PDF
    class AddAttachmentConfig : ActionConfig
    {
        public string FilePath { get; set; } = null!;          // Path to the file to attach
        public string Description { get; set; } = null!;       // Description of the attachment
    }

    // Action to change a viewer preference
    class ViewerPreferenceConfig : ActionConfig
    {
        // ViewerPreference enum values are represented as int; default 0 is a safe placeholder.
        public int Preference { get; set; } = 0;
    }

    // Action to add an annotation (text or file attachment)
    class AddAnnotationConfig : ActionConfig
    {
        public string AnnotationSubtype { get; set; } = null!; // "Text" or "FileAttachment"
        public Aspose.Pdf.Rectangle? Rect { get; set; }        // Position on the page (nullable, will be validated at runtime)
        public string? Content { get; set; }                  // Text content or file path
        public string? Title { get; set; }                    // Title for the annotation (optional)
        public string? Description { get; set; }             // Description for file attachment (optional)
    }

    // Root configuration for the pipeline
    class PipelineConfig
    {
        public string InputPdf { get; set; } = null!;          // Source PDF file
        public string OutputPdf { get; set; } = null!;         // Destination PDF file
        public List<ActionConfig> Actions { get; set; } = new List<ActionConfig>();
    }

    class Program
    {
        static void Main()
        {
            // -----------------------------------------------------------------
            // Sample configuration – in a real scenario this could be read from
            // JSON, XML, database, etc.
            // -----------------------------------------------------------------
            PipelineConfig config = new PipelineConfig
            {
                InputPdf = "source.pdf",
                OutputPdf = "result.pdf",
                Actions = new List<ActionConfig>
                {
                    new AddAttachmentConfig
                    {
                        ActionType = "AddAttachment",
                        FilePath = "attachment.docx",
                        Description = "Sample attachment"
                    },
                    new ViewerPreferenceConfig
                    {
                        ActionType = "ViewerPreference",
                        Preference = ViewerPreference.HideMenubar // hide the menubar
                    },
                    new AddAnnotationConfig
                    {
                        ActionType = "AddAnnotation",
                        AnnotationSubtype = "Text",
                        Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550),
                        Content = "This is a text annotation",
                        Title = "Note"
                    },
                    new AddAnnotationConfig
                    {
                        ActionType = "AddAnnotation",
                        AnnotationSubtype = "FileAttachment",
                        Rect = new Aspose.Pdf.Rectangle(50, 700, 150, 800),
                        Content = "attachment.docx", // file to attach as annotation
                        Title = "Attached Document",
                        Description = "File attachment annotation"
                    }
                }
            };

            // Validate input file existence
            if (!File.Exists(config.InputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {config.InputPdf}");
                return;
            }

            // -----------------------------------------------------------------
            // Execute the pipeline using PdfContentEditor (Facade API)
            // -----------------------------------------------------------------
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the source PDF
                editor.BindPdf(config.InputPdf);

                // Process each configured action in order
                foreach (var action in config.Actions)
                {
                    switch (action.ActionType)
                    {
                        case "AddAttachment":
                            var attach = action as AddAttachmentConfig;
                            if (attach != null && !string.IsNullOrEmpty(attach.FilePath) && File.Exists(attach.FilePath))
                            {
                                // Add a document attachment without a visible annotation
                                editor.AddDocumentAttachment(attach.FilePath, attach.Description ?? string.Empty);
                            }
                            break;

                        case "ViewerPreference":
                            var pref = action as ViewerPreferenceConfig;
                            if (pref != null)
                            {
                                // Change viewer preference using the integer value from ViewerPreference
                                editor.ChangeViewerPreference(pref.Preference);
                            }
                            break;

                        case "AddAnnotation":
                            var ann = action as AddAnnotationConfig;
                            if (ann != null && ann.Rect != null)
                            {
                                // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle because PdfContentEditor expects the latter
                                System.Drawing.Rectangle sysRect = new System.Drawing.Rectangle(
                                    (int)ann.Rect.LLX,
                                    (int)ann.Rect.LLY,
                                    (int)(ann.Rect.URX - ann.Rect.LLX),
                                    (int)(ann.Rect.URY - ann.Rect.LLY));

                                if (string.Equals(ann.AnnotationSubtype, "Text", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Create a text annotation on the first page (page index is 1‑based)
                                    editor.CreateText(
                                        sysRect,
                                        ann.Content ?? string.Empty,
                                        ann.Title ?? string.Empty,
                                        true,                     // set to true to display the annotation initially
                                        ann.Description ?? string.Empty,
                                        0);                       // default annotation flags
                                }
                                else if (string.Equals(ann.AnnotationSubtype, "FileAttachment", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Create a file attachment annotation on the first page
                                    editor.CreateFileAttachment(
                                        sysRect,
                                        ann.Title ?? string.Empty,
                                        ann.Content ?? string.Empty, // path to the file to embed
                                        0,                            // annotation flags
                                        ann.Description ?? string.Empty);
                                }
                            }
                            break;

                        default:
                            // Unknown action – ignore or log
                            Console.WriteLine($"Unsupported action type: {action.ActionType}");
                            break;
                    }
                }

                // Save the modified PDF to the output path
                editor.Save(config.OutputPdf);
            }

            Console.WriteLine($"Pipeline completed. Output saved to '{config.OutputPdf}'.");
        }
    }
}
