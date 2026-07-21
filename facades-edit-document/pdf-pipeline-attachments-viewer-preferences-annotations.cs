using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing; // Needed for System.Drawing.Rectangle used by PdfContentEditor
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfPipelineExample
{
    // Simple configuration classes
    public class AttachmentConfig
    {
        // Made nullable to satisfy the compiler warnings for non‑nullable reference types.
        public string? FilePath { get; set; }
        public string? Description { get; set; }
    }

    public class ViewerPreferenceConfig
    {
        public int Preference { get; set; } // Value from ViewerPreference class
    }

    public class AnnotationConfig
    {
        public enum AnnotationType { FileAttachment, FreeText, WebLink }

        public AnnotationType Type { get; set; }
        public int PageNumber { get; set; } // 1‑based page index
        public Aspose.Pdf.Rectangle? Rect { get; set; } // Position on the page (nullable)

        // Specific to each annotation type – all nullable because they are not required for every type.
        public string? FilePath { get; set; }   // For FileAttachment
        public string? Description { get; set; } // For FileAttachment
        public string? Text { get; set; }        // For FreeText or WebLink
        public string? Url { get; set; }         // For WebLink
    }

    public class PipelineConfig
    {
        public string InputPdf { get; set; } = string.Empty;
        public string OutputPdf { get; set; } = string.Empty;
        public List<AttachmentConfig> Attachments { get; set; } = new List<AttachmentConfig>();
        public List<ViewerPreferenceConfig> ViewerPreferences { get; set; } = new List<ViewerPreferenceConfig>();
        public List<AnnotationConfig> Annotations { get; set; } = new List<AnnotationConfig>();
    }

    class Program
    {
        static void Main()
        {
            // -----------------------------------------------------------------
            // Build a sample configuration (in real scenarios this could be
            // deserialized from JSON, XML, etc.).
            // -----------------------------------------------------------------
            PipelineConfig config = new PipelineConfig
            {
                InputPdf = "input.pdf",
                OutputPdf = "output.pdf",
                Attachments =
                {
                    new AttachmentConfig
                    {
                        FilePath = "attachment1.pdf",
                        Description = "First attachment"
                    },
                    new AttachmentConfig
                    {
                        FilePath = "attachment2.txt",
                        Description = "Second attachment"
                    }
                },
                ViewerPreferences =
                {
                    new ViewerPreferenceConfig
                    {
                        Preference = ViewerPreference.HideMenubar
                    },
                    new ViewerPreferenceConfig
                    {
                        Preference = ViewerPreference.PageModeUseNone
                    }
                },
                Annotations =
                {
                    new AnnotationConfig
                    {
                        Type = AnnotationConfig.AnnotationType.FileAttachment,
                        PageNumber = 1,
                        Rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550),
                        FilePath = "note.txt",
                        Description = "Attached note"
                    },
                    new AnnotationConfig
                    {
                        Type = AnnotationConfig.AnnotationType.FreeText,
                        PageNumber = 2,
                        Rect = new Aspose.Pdf.Rectangle(200, 400, 400, 450),
                        Text = "Important note"
                    },
                    new AnnotationConfig
                    {
                        Type = AnnotationConfig.AnnotationType.WebLink,
                        PageNumber = 3,
                        Rect = new Aspose.Pdf.Rectangle(50, 700, 250, 750),
                        Url = "https://www.example.com",
                        Text = "Visit Example"
                    }
                }
            };

            // -----------------------------------------------------------------
            // Validate input file existence.
            // -----------------------------------------------------------------
            if (!File.Exists(config.InputPdf))
            {
                Console.Error.WriteLine($"Input PDF not found: {config.InputPdf}");
                return;
            }

            // -----------------------------------------------------------------
            // Execute the pipeline using PdfContentEditor (facade API).
            // -----------------------------------------------------------------
            PdfContentEditor editor = new PdfContentEditor();
            try
            {
                // Bind the source PDF.
                editor.BindPdf(config.InputPdf);

                // 1) Add document attachments (no visual annotation).
                foreach (var att in config.Attachments)
                {
                    if (string.IsNullOrWhiteSpace(att.FilePath))
                    {
                        Console.Error.WriteLine("Attachment file path is missing.");
                        continue;
                    }

                    if (!File.Exists(att.FilePath))
                    {
                        Console.Error.WriteLine($"Attachment file not found: {att.FilePath}");
                        continue;
                    }

                    // Description may be null – the API accepts null.
                    editor.AddDocumentAttachment(att.FilePath, att.Description);
                }

                // 2) Change viewer preferences.
                foreach (var vp in config.ViewerPreferences)
                {
                    editor.ChangeViewerPreference(vp.Preference);
                }

                // 3) Insert annotations as defined in the configuration.
                foreach (var ann in config.Annotations)
                {
                    // Guard against missing rectangle – the API will throw otherwise.
                    if (ann.Rect == null)
                    {
                        Console.Error.WriteLine("Annotation rectangle is not defined; skipping annotation.");
                        continue;
                    }

                    // Convert Aspose.Pdf.Rectangle to System.Drawing.Rectangle because PdfContentEditor expects the latter.
                    System.Drawing.Rectangle sysRect = ConvertToSystemRectangle(ann.Rect);

                    switch (ann.Type)
                    {
                        case AnnotationConfig.AnnotationType.FileAttachment:
                            if (string.IsNullOrWhiteSpace(ann.FilePath))
                            {
                                Console.Error.WriteLine("FileAttachment annotation missing file path; skipping.");
                                continue;
                            }
                            // Create a file attachment annotation on the specified page.
                            editor.CreateFileAttachment(
                                sysRect,
                                ann.FilePath,
                                ann.Description,
                                ann.PageNumber,
                                Path.GetFileName(ann.FilePath));
                            break;

                        case AnnotationConfig.AnnotationType.FreeText:
                            if (string.IsNullOrWhiteSpace(ann.Text))
                            {
                                Console.Error.WriteLine("FreeText annotation missing text; skipping.");
                                continue;
                            }
                            // Create a free‑text annotation.
                            editor.CreateFreeText(
                                sysRect,
                                ann.Text,
                                ann.PageNumber);
                            break;

                        case AnnotationConfig.AnnotationType.WebLink:
                            if (string.IsNullOrWhiteSpace(ann.Url))
                            {
                                Console.Error.WriteLine("WebLink annotation missing URL; skipping.");
                                continue;
                            }
                            // Create a web link annotation.
                            editor.CreateWebLink(
                                sysRect,
                                ann.Url,
                                ann.PageNumber);
                            // Optionally add a visible text annotation at the same location.
                            if (!string.IsNullOrWhiteSpace(ann.Text))
                            {
                                editor.CreateFreeText(
                                    sysRect,
                                    ann.Text,
                                    ann.PageNumber);
                            }
                            break;
                    }
                }

                // Save the modified PDF.
                editor.Save(config.OutputPdf);
                Console.WriteLine($"Pipeline completed. Output saved to '{config.OutputPdf}'.");
            }
            finally
            {
                // Ensure resources are released.
                editor.Close();
            }
        }

        /// <summary>
        /// Converts an Aspose.Pdf.Rectangle (which uses double coordinates) to a System.Drawing.Rectangle (which uses int coordinates).
        /// The conversion truncates fractional parts, which is acceptable for the PdfContentEditor overloads.
        /// </summary>
        private static System.Drawing.Rectangle ConvertToSystemRectangle(Aspose.Pdf.Rectangle rect)
        {
            int x = (int)rect.LLX;
            int y = (int)rect.LLY;
            int width = (int)(rect.URX - rect.LLX);
            int height = (int)(rect.URY - rect.LLY);
            return new System.Drawing.Rectangle(x, y, width, height);
        }
    }
}
