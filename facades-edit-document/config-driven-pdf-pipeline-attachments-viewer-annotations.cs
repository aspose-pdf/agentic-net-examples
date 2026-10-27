using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachmentConfig
{
    public required string FilePath { get; set; }
    public required string Description { get; set; }
}

class ViewerPreferencesConfig
{
    public bool HideToolbar { get; set; }
    public bool HideMenubar { get; set; }
    public bool CenterWindow { get; set; }
    public bool DisplayDocTitle { get; set; }
    // Add other preferences as needed
}

class AnnotationConfig
{
    public int PageNumber { get; set; }          // 1‑based page index
    public double Llx { get; set; }              // lower‑left x
    public double Lly { get; set; }              // lower‑left y
    public double Urx { get; set; }              // upper‑right x
    public double Ury { get; set; }              // upper‑right y
    public required string Title { get; set; }
    public required string Contents { get; set; }
}

class PipelineConfig
{
    public required string InputPdf { get; set; }
    public required string OutputPdf { get; set; }
    public List<AttachmentConfig> Attachments { get; set; } = new List<AttachmentConfig>();
    public ViewerPreferencesConfig ViewerPrefs { get; set; } = new ViewerPreferencesConfig();
    public List<AnnotationConfig> Annotations { get; set; } = new List<AnnotationConfig>();
}

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Define a simple configuration (in real scenarios this could be
        // deserialized from JSON, XML, etc.)
        // -----------------------------------------------------------------
        PipelineConfig config = new PipelineConfig {
            InputPdf = "input.pdf",
            OutputPdf = "output.pdf",
            Attachments = new List<AttachmentConfig>
            {
                new AttachmentConfig { FilePath = "attachment1.txt", Description = "First attachment" },
                new AttachmentConfig { FilePath = "attachment2.jpg", Description = "Image attachment" }
            },
            ViewerPrefs = new ViewerPreferencesConfig
            {
                HideToolbar = true,
                HideMenubar = true,
                CenterWindow = true,
                DisplayDocTitle = true
            },
            Annotations = new List<AnnotationConfig>
            {
                new AnnotationConfig
                {
                    PageNumber = 1,
                    Llx = 100, Lly = 500, Urx = 300, Ury = 550,
                    Title = "Note 1",
                    Contents = "This is a sample text annotation."
                },
                new AnnotationConfig
                {
                    PageNumber = 2,
                    Llx = 150, Lly = 400, Urx = 350, Ury = 450,
                    Title = "Note 2",
                    Contents = "Another annotation on page 2."
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
        // Load the PDF using the core Document API (document‑disposal‑with‑using rule)
        // -----------------------------------------------------------------
        using (Document doc = new Document(config.InputPdf))
        {
            // -------------------------------------------------------------
            // Step 1: Add attachments (using EmbeddedFiles collection)
            // -------------------------------------------------------------
            foreach (var att in config.Attachments)
            {
                if (!File.Exists(att.FilePath))
                {
                    Console.Error.WriteLine($"Attachment not found: {att.FilePath}");
                    continue; // skip missing attachments
                }

                // Create a FileSpecification with the file name and description.
                var fileSpec = new FileSpecification(Path.GetFileName(att.FilePath))
                {
                    Description = att.Description,
                    // Embed the actual file bytes.
                    Contents = new MemoryStream(File.ReadAllBytes(att.FilePath))
                };
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // -------------------------------------------------------------
            // Step 2: Set viewer preferences – use reflection to stay
            //         compatible with older Aspose.Pdf versions that may not
            //         expose the ViewerPreferences type.
            // -------------------------------------------------------------
            ApplyViewerPreferences(doc, config.ViewerPrefs);

            // -------------------------------------------------------------
            // Step 3: Insert annotations using the core Document API.
            // -------------------------------------------------------------
            foreach (var ann in config.Annotations)
            {
                if (ann.PageNumber < 1 || ann.PageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number {ann.PageNumber} for annotation.");
                    continue;
                }

                var rect = new Aspose.Pdf.Rectangle(ann.Llx, ann.Lly, ann.Urx, ann.Ury);
                var txtAnn = new TextAnnotation(doc.Pages[ann.PageNumber], rect)
                {
                    Title = ann.Title,
                    Contents = ann.Contents,
                    Open = true,
                    Icon = TextIcon.Note
                };
                doc.Pages[ann.PageNumber].Annotations.Add(txtAnn);
            }

            // Save the final result
            doc.Save(config.OutputPdf);
        }

        Console.WriteLine($"Pipeline completed. Output saved to '{config.OutputPdf}'.");
    }

    /// <summary>
    /// Applies viewer preferences to a Document using reflection. This avoids a hard
    /// compile‑time dependency on the ViewerPreferences type, which may be absent in
    /// older Aspose.Pdf releases.
    /// </summary>
    private static void ApplyViewerPreferences(Document doc, ViewerPreferencesConfig cfg)
    {
        // Try to get the "ViewerPreferences" property (it may not exist).
        var vpProp = doc.GetType().GetProperty("ViewerPreferences");
        if (vpProp == null) return; // Property not available – nothing to set.

        var vpInstance = vpProp.GetValue(doc);
        if (vpInstance == null) return;

        // Helper to set a boolean property if it exists.
        void SetBool(string name, bool value)
        {
            var prop = vpInstance.GetType().GetProperty(name);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(vpInstance, value);
            }
        }

        SetBool("HideToolbar", cfg.HideToolbar);
        SetBool("HideMenubar", cfg.HideMenubar);
        SetBool("CenterWindow", cfg.CenterWindow);
        SetBool("DisplayDocTitle", cfg.DisplayDocTitle);
        // Add more preferences here following the same pattern.
    }
}
