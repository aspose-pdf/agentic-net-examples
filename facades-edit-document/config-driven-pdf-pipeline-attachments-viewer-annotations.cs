using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;

// Configuration models
class AttachmentConfig
{
    public required string FilePath { get; set; }
    public required string Description { get; set; }
}

class ViewerPrefConfig
{
    public bool HideToolbar { get; set; }
    public bool HideMenubar { get; set; }
    public bool HideWindowUI { get; set; }
    public bool FitWindow { get; set; }
    public bool CenterWindow { get; set; }
    public bool DisplayDocTitle { get; set; }
    public PageMode PageMode { get; set; } = PageMode.UseNone;
    public PageLayout PageLayout { get; set; } = PageLayout.SinglePage;
}

class AnnotationConfig
{
    public int PageNumber { get; set; }               // 1‑based page index
    public double Llx { get; set; }                   // lower‑left X
    public double Lly { get; set; }                   // lower‑left Y
    public double Urx { get; set; }                   // upper‑right X
    public double Ury { get; set; }                   // upper‑right Y
    public required string Title { get; set; }
    public required string Contents { get; set; }
    public TextIcon Icon { get; set; } = TextIcon.Note;
    public bool Open { get; set; } = true;
}

// Pipeline implementation
class PdfProcessingPipeline
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Copy input to output so we work on a writable file
        File.Copy(inputPdf, outputPdf, true);

        // ----- 1. Define configuration -----
        var attachments = new[]
        {
            new AttachmentConfig { FilePath = "attachment1.txt", Description = "First attachment" },
            new AttachmentConfig { FilePath = "attachment2.jpg", Description = "Second attachment" }
        };

        ViewerPrefConfig viewerPrefs = new ViewerPrefConfig
        {
            HideToolbar = true,
            HideMenubar = true,
            HideWindowUI = false,
            FitWindow = true,
            CenterWindow = true,
            DisplayDocTitle = true,
            PageMode = PageMode.UseOutlines,
            PageLayout = PageLayout.TwoColumnLeft
        };

        var annotations = new[]
        {
            new AnnotationConfig
            {
                PageNumber = 1,
                Llx = 100, Lly = 500, Urx = 300, Ury = 550,
                Title = "Note 1",
                Contents = "This is the first annotation.",
                Icon = TextIcon.Comment,
                Open = true
            },
            new AnnotationConfig
            {
                PageNumber = 2,
                Llx = 150, Lly = 400, Urx = 350, Ury = 450,
                Title = "Note 2",
                Contents = "Second page annotation.",
                Icon = TextIcon.Key,
                Open = false
            }
        };

        try
        {
            // Load the PDF document (Aspose.Pdf.Document works with all operations we need)
            Document doc = new Document(outputPdf);

            // ----- 2. Add attachments (embedded files) -----
            foreach (var att in attachments)
            {
                if (!File.Exists(att.FilePath))
                {
                    Console.Error.WriteLine($"Attachment not found: {att.FilePath}");
                    continue;
                }

                // Create a FileSpecification with a stream containing the file data and add it to EmbeddedFiles.
                var fileSpec = new FileSpecification(Path.GetFileName(att.FilePath), att.Description);
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(att.FilePath));
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // ----- 3. Set viewer preferences (guarded for older Aspose.Pdf versions) -----
            try
            {
                dynamic dynDoc = doc; // use dynamic to avoid compile‑time binding if the property is missing
                dynDoc.ViewerPreferences.HideToolbar = viewerPrefs.HideToolbar;
                dynDoc.ViewerPreferences.HideMenubar = viewerPrefs.HideMenubar;
                dynDoc.ViewerPreferences.HideWindowUI = viewerPrefs.HideWindowUI;
                dynDoc.ViewerPreferences.FitWindow = viewerPrefs.FitWindow;
                dynDoc.ViewerPreferences.CenterWindow = viewerPrefs.CenterWindow;
                dynDoc.ViewerPreferences.DisplayDocTitle = viewerPrefs.DisplayDocTitle;
                dynDoc.ViewerPreferences.PageMode = viewerPrefs.PageMode;
                dynDoc.ViewerPreferences.PageLayout = viewerPrefs.PageLayout;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version. Skipping viewer‑preference configuration.");
            }

            // ----- 4. Insert annotations -----
            foreach (var ann in annotations)
            {
                if (ann.PageNumber < 1 || ann.PageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine($"Invalid page number for annotation: {ann.PageNumber}");
                    continue;
                }

                Page page = doc.Pages[ann.PageNumber];
                var rect = new Rectangle(ann.Llx, ann.Lly, ann.Urx, ann.Ury);
                var textAnnot = new TextAnnotation(page, rect)
                {
                    Title = ann.Title,
                    Contents = ann.Contents,
                    Open = ann.Open,
                    Icon = ann.Icon
                };
                page.Annotations.Add(textAnnot);
            }

            // Save the modified document
            doc.Save(outputPdf);

            Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
