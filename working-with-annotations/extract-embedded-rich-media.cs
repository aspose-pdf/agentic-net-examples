using System;
using System.IO;
using System.Reflection;
using System.Collections;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputRoot = "ExtractedMedia";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];
                    string pageFolder = Path.Combine(outputRoot, $"Page_{pageIndex}");
                    Directory.CreateDirectory(pageFolder);

                    int annotationIndex = 0;
                    foreach (Annotation annotation in page.Annotations)
                    {
                        annotationIndex++;

                        // Process only RichMediaAnnotations
                        if (annotation is RichMediaAnnotation richMedia)
                        {
                            string annotFolder = Path.Combine(pageFolder, $"Annotation_{annotationIndex}");
                            Directory.CreateDirectory(annotFolder);

                            ExtractRichMediaAssets(richMedia, annotFolder);
                        }
                    }
                }
            }

            Console.WriteLine("Media extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Uses reflection to stay compatible with Aspose.Pdf versions that may not expose RichMediaContent/Asset types.
    private static void ExtractRichMediaAssets(RichMediaAnnotation richMedia, string targetFolder)
    {
        // Get the RichMediaContent property via reflection.
        PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent", BindingFlags.Public | BindingFlags.Instance);
        if (contentProp == null)
            return; // Property not available – nothing to extract.

        object richMediaContent = contentProp.GetValue(richMedia);
        if (richMediaContent == null)
            return;

        // Get the Assets collection from the content object.
        PropertyInfo assetsProp = richMediaContent.GetType().GetProperty("Assets", BindingFlags.Public | BindingFlags.Instance);
        if (assetsProp == null)
            return;

        object assetsObj = assetsProp.GetValue(richMediaContent);
        if (assetsObj is not IEnumerable assets)
            return;

        int assetIndex = 0;
        foreach (object asset in assets)
        {
            // Asset properties are accessed via reflection to avoid compile‑time dependencies.
            string assetName = GetAssetPropertyValue(asset, "Name") as string;
            if (string.IsNullOrEmpty(assetName))
                assetName = $"Asset_{assetIndex}";

            string mime = GetAssetPropertyValue(asset, "MimeType") as string;
            string extension = GetExtensionFromMime(mime);
            string filePath = Path.Combine(targetFolder, $"{assetName}{extension}");

            // Retrieve the embedded stream.
            MethodInfo getFileMethod = asset.GetType().GetMethod("GetFile", Type.EmptyTypes);
            if (getFileMethod == null)
                continue;

            using (Stream assetStream = getFileMethod.Invoke(asset, null) as Stream)
            using (FileStream fileStream = File.Create(filePath))
            {
                assetStream?.CopyTo(fileStream);
            }

            assetIndex++;
        }
    }

    // Helper to read a property value via reflection safely.
    private static object GetAssetPropertyValue(object asset, string propertyName)
    {
        PropertyInfo prop = asset.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        return prop?.GetValue(asset);
    }

    // Simple MIME‑type to file‑extension mapper
    static string GetExtensionFromMime(string mime)
    {
        if (string.IsNullOrEmpty(mime))
            return string.Empty;

        switch (mime.ToLowerInvariant())
        {
            case "audio/mpeg": return ".mp3";
            case "audio/mp4": return ".m4a";
            case "video/mp4": return ".mp4";
            case "application/pdf": return ".pdf";
            case "image/jpeg": return ".jpg";
            case "image/png": return ".png";
            case "application/x-shockwave-flash": return ".swf";
            default: return string.Empty;
        }
    }
}
