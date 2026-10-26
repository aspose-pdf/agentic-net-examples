using System;
using System.IO;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Annotations collection may be empty
                if (page.Annotations == null) continue;

                foreach (Annotation annot in page.Annotations)
                {
                    // Get the concrete annotation type name
                    string typeName = annot.GetType().Name;

                    // Use fully qualified Rectangle to avoid ambiguity
                    Aspose.Pdf.Rectangle rect = annot.Rect;

                    // Retrieve associated media file name when applicable
                    string mediaFile = GetMediaFileName(annot);

                    Console.WriteLine(
                        $"Page {i}: Type={typeName}, Rect=[{rect.LLX},{rect.LLY},{rect.URX},{rect.URY}], Media={mediaFile}");
                }
            }
        }
    }

    // Helper method to extract a media file name from known annotation types
    private static string GetMediaFileName(Annotation annot)
    {
        switch (annot)
        {
            case FileAttachmentAnnotation fileAnnot:
                // The File property returns a FileSpecification; use its Name property
                return fileAnnot.File?.Name ?? "(none)";

            case RichMediaAnnotation richAnnot:
                // RichMediaContent may not exist in older Aspose.Pdf versions – use reflection
                var richContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                if (richContentProp != null)
                {
                    var richContent = richContentProp.GetValue(richAnnot);
                    if (richContent != null)
                    {
                        var assetsProp = richContent.GetType().GetProperty("Assets");
                        var assets = assetsProp?.GetValue(richContent) as IEnumerable;
                        if (assets != null)
                        {
                            var enumerator = assets.GetEnumerator();
                            if (enumerator.MoveNext())
                            {
                                var asset = enumerator.Current;
                                // Asset may expose FileName or Name – try both
                                var nameProp = asset.GetType().GetProperty("FileName") ??
                                                asset.GetType().GetProperty("Name");
                                var name = nameProp?.GetValue(asset) as string;
                                return name ?? "(none)";
                            }
                        }
                    }
                }
                return "(none)";

            default:
                // Other annotation types do not reference external media
                return "(n/a)";
        }
    }
}
