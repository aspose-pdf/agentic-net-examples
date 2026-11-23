using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string multimediaPath = "sample_video.mp4"; // path to multimedia file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(multimediaPath))
        {
            Console.Error.WriteLine($"Multimedia file not found: {multimediaPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // Add a Rich Media (multimedia) annotation if the API is available
            // -------------------------------------------------
            // Define the rectangle where the annotation will appear
            var mediaRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the annotation (always available)
            var richMedia = new RichMediaAnnotation(page, mediaRect);

            // Try to attach multimedia content via reflection – works only on newer Aspose.Pdf versions
            TryAddRichMediaContent(richMedia, multimediaPath);

            // Add the annotation to the page (valid even when no media is attached)
            page.Annotations.Add(richMedia);

            // -------------------------------------------------
            // Add a sticky note annotation (TextAnnotation)
            // -------------------------------------------------
            var stickyRect = new Aspose.Pdf.Rectangle(350, 500, 400, 550);
            var stickyNote = new TextAnnotation(page, stickyRect)
            {
                Title = "Reminder",
                Contents = "Review this section later.",
                Icon = TextIcon.Note,
                Open = true
            };
            page.Annotations.Add(stickyNote);

            // -------------------------------------------------
            // Delete the sticky note annotation by reference
            // -------------------------------------------------
            page.Annotations.Remove(stickyNote);

            // -------------------------------------------------
            // Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Attempts to add multimedia content to a RichMediaAnnotation using reflection.
    /// This avoids compile‑time dependencies on RichMediaContent / RichMediaConfiguration types
    /// that may be absent in older Aspose.Pdf versions.
    /// </summary>
    private static void TryAddRichMediaContent(RichMediaAnnotation annotation, string mediaFilePath)
    {
        // Look for the RichMediaContent type in the Aspose.Pdf assembly
        var contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
        if (contentType == null)
            return; // API not present – skip media attachment

        // Create an instance of RichMediaContent
        var richMediaContent = Activator.CreateInstance(contentType);
        if (richMediaContent == null)
            return;

        // Call AddFile(string) to embed the multimedia file
        var addFileMethod = contentType.GetMethod("AddFile", new[] { typeof(string) });
        addFileMethod?.Invoke(richMediaContent, new object[] { mediaFilePath });

        // Create RichMediaConfiguration if the type exists
        var configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
        object configInstance = null;
        if (configType != null)
        {
            var ctor = configType.GetConstructor(new[] { contentType });
            if (ctor != null)
                configInstance = ctor.Invoke(new[] { richMediaContent });
        }

        // Prefer setting the RichMediaConfiguration property; if not present, fall back to RichMediaContent property
        var configProp = typeof(RichMediaAnnotation).GetProperty("RichMediaConfiguration");
        if (configProp != null && configInstance != null)
        {
            configProp.SetValue(annotation, configInstance);
        }
        else
        {
            var contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
            if (contentProp != null)
                contentProp.SetValue(annotation, richMediaContent);
        }
    }
}
