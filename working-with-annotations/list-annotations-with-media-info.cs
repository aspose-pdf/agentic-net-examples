using System;
using System.IO;
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Each page has an Annotations collection
                foreach (Annotation annotation in page.Annotations)
                {
                    // Annotation type name
                    string typeName = annotation.GetType().Name;

                    // Rectangle (position) of the annotation
                    Aspose.Pdf.Rectangle rect = annotation.Rect;
                    string rectInfo = $"LLX={rect.LLX}, LLY={rect.LLY}, URX={rect.URX}, URY={rect.URY}";

                    // Retrieve an associated media file name (if any)
                    string mediaFileName = string.Empty;

                    // FileAttachmentAnnotation – use the File property (FileSpecification) and its Name
                    if (annotation is FileAttachmentAnnotation fileAttachment && fileAttachment.File != null)
                    {
                        mediaFileName = fileAttachment.File.Name ?? string.Empty;
                    }
                    // RichMediaAnnotation – the RichMediaContent type may not exist in older versions.
                    // Use reflection to safely obtain the first asset's file name when possible.
                    else if (annotation is RichMediaAnnotation richMedia)
                    {
                        var richContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                        if (richContentProp != null)
                        {
                            var richContent = richContentProp.GetValue(richMedia);
                            if (richContent != null)
                            {
                                var assetsProp = richContent.GetType().GetProperty("Assets");
                                var assets = assetsProp?.GetValue(richContent) as System.Collections.IList;
                                if (assets != null && assets.Count > 0)
                                {
                                    var firstAsset = assets[0];
                                    var fileNameProp = firstAsset.GetType().GetProperty("FileName");
                                    if (fileNameProp != null)
                                    {
                                        mediaFileName = fileNameProp.GetValue(firstAsset) as string ?? string.Empty;
                                    }
                                }
                            }
                        }
                    }

                    // Log the details
                    Console.WriteLine($"Page {i}: Type={typeName}, Rect=[{rectInfo}], MediaFile=\"{mediaFileName}\"");
                }
            }
        }
    }
}
