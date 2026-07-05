using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PipelineConfig
{
    // Paths for source PDF and output PDF
    public string? InputPdf { get; set; }
    public string? OutputPdf { get; set; }

    // Attachments to add (file path + description)
    public List<(string FilePath, string Description)> Attachments { get; set; } = new List<(string, string)>();

    // Viewer preferences (values from ViewerPreference class)
    public List<int> ViewerPreferences { get; set; } = new List<int>();

    // Simple annotation definition – file attachment annotation as example
    public List<AnnotationConfig> Annotations { get; set; } = new List<AnnotationConfig>();
}

class AnnotationConfig
{
    // Rectangle where the annotation will appear (llx, lly, urx, ury)
    public double Llx { get; set; }
    public double Lly { get; set; }
    public double Urx { get; set; }
    public double Ury { get; set; }

    // File to attach as annotation
    public string? AttachmentFile { get; set; }

    // Description shown in the annotation popup
    public string? Description { get; set; }

    // Page number (1‑based)
    public int PageNumber { get; set; }

    // Name of the annotation (used as internal identifier)
    public string? Name { get; set; }
}

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Build a sample configuration – in real scenarios this could be
        // read from JSON, XML, database, etc.
        // -----------------------------------------------------------------
        PipelineConfig config = new PipelineConfig
        {
            InputPdf = "source.pdf",
            OutputPdf = "processed.pdf",
            Attachments =
            {
                ("attachment1.docx", "First attachment"),
                ("attachment2.xlsx", "Second attachment")
            },
            ViewerPreferences =
            {
                ViewerPreference.HideMenubar,
                ViewerPreference.PageModeUseNone
            },
            Annotations =
            {
                new AnnotationConfig
                {
                    Llx = 100, Lly = 500, Urx = 200, Ury = 550,
                    AttachmentFile = "note.txt",
                    Description = "Note attached as annotation",
                    PageNumber = 1,
                    Name = "NoteAnnotation"
                }
            }
        };

        // Validate input file existence
        if (string.IsNullOrEmpty(config.InputPdf) || !File.Exists(config.InputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {config.InputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Execute the pipeline using PdfContentEditor (facade)
        // -----------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF
            editor.BindPdf(config.InputPdf);

            // 1) Add document attachments (no visual annotation)
            foreach (var (filePath, description) in config.Attachments)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Attachment not found: {filePath}");
                    continue;
                }

                editor.AddDocumentAttachment(filePath, description);
            }

            // 2) Apply viewer preferences sequentially
            foreach (int pref in config.ViewerPreferences)
            {
                editor.ChangeViewerPreference(pref);
            }

            // 3) Insert annotations – using file‑attachment annotation as example
            foreach (var ann in config.Annotations)
            {
                if (string.IsNullOrEmpty(ann.AttachmentFile) || !File.Exists(ann.AttachmentFile))
                {
                    Console.Error.WriteLine($"Annotation attachment not found: {ann.AttachmentFile}");
                    continue;
                }

                // Convert the double coordinates to a System.Drawing.Rectangle.
                // Rectangle constructor expects (x, y, width, height).
                int x = (int)Math.Round(ann.Llx);
                int y = (int)Math.Round(ann.Lly);
                int width = (int)Math.Round(ann.Urx - ann.Llx);
                int height = (int)Math.Round(ann.Ury - ann.Lly);
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x, y, width, height);

                // Create a file‑attachment annotation on the specified page.
                // Parameters: rectangle, file path, description, page number, annotation name.
                editor.CreateFileAttachment(
                    rect,
                    ann.AttachmentFile,
                    ann.Description ?? string.Empty,
                    ann.PageNumber,
                    ann.Name ?? string.Empty);
            }

            // Save the modified PDF
            editor.Save(config.OutputPdf);
        }

        Console.WriteLine($"Pipeline completed. Output saved to '{config.OutputPdf}'.");
    }
}
