using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string mediaPath = "media.swf";   // Rich media file
        const string posterPath = "poster.jpg"; // Poster image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }
        if (!File.Exists(posterPath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 16 pages (1‑based indexing)
            if (doc.Pages.Count < 16)
            {
                Console.Error.WriteLine("The PDF does not contain page 16.");
                return;
            }

            // Get page 16
            Page page = doc.Pages[16];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create the RichMediaAnnotation on the specified page
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // Attach rich‑media content using reflection.
            // The RichMediaContent, RichMediaConfiguration and RichMediaAsset
            // classes are not present in older Aspose.Pdf versions.  By using
            // reflection we keep the code compilable on all supported versions
            // while still taking advantage of the feature when it is available.
            // -----------------------------------------------------------------
            try
            {
                // Look for the RichMediaContent type
                Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (richMediaContentType != null)
                {
                    // Create an instance of RichMediaContent
                    object richMediaContent = Activator.CreateInstance(richMediaContentType);

                    // The newer API uses a RichMediaConfiguration that holds assets.
                    // We try to locate the configuration type and the AddAsset method.
                    Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                    if (configType != null)
                    {
                        object configInstance = Activator.CreateInstance(configType);

                        // Locate the RichMediaAsset type and its constructor that accepts a file path.
                        Type assetType = Type.GetType("Aspose.Pdf.Annotations.RichMediaAsset, Aspose.Pdf");
                        if (assetType != null)
                        {
                            // Create the asset (media file)
                            object assetInstance = Activator.CreateInstance(assetType, mediaPath);

                            // Call config.AddAsset(assetInstance)
                            MethodInfo addAssetMethod = configType.GetMethod("AddAsset", new[] { assetType });
                            addAssetMethod?.Invoke(configInstance, new[] { assetInstance });
                        }
                        else
                        {
                            // Fallback: older versions expose a simple AddFile(string) method on RichMediaContent.
                            MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                            addFileMethod?.Invoke(richMediaContent, new object[] { mediaPath });
                        }

                        // Assign the configuration to the content (property "Configuration")
                        PropertyInfo configProp = richMediaContentType.GetProperty("Configuration");
                        configProp?.SetValue(richMediaContent, configInstance);
                    }
                    else
                    {
                        // Very old versions: just add the file directly.
                        MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                        addFileMethod?.Invoke(richMediaContent, new object[] { mediaPath });
                    }

                    // Finally, set the RichMediaContent property on the annotation.
                    PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    contentProp?.SetValue(richMedia, richMediaContent);
                }
                else
                {
                    Console.Error.WriteLine("RichMediaContent type not found – media will not be attached, but annotation will be created.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while configuring rich media via reflection: {ex.Message}");
            }

            // -----------------------------------------------------------------
            // Set the poster image using reflection – the RichMediaPoster class
            // and the Poster property may be missing in older releases.
            // -----------------------------------------------------------------
            try
            {
                Type posterType = Type.GetType("Aspose.Pdf.Annotations.RichMediaPoster, Aspose.Pdf");
                PropertyInfo posterProp = typeof(RichMediaAnnotation).GetProperty("Poster");
                if (posterType != null && posterProp != null && posterProp.CanWrite)
                {
                    object posterInstance = Activator.CreateInstance(posterType, posterPath);
                    posterProp.SetValue(richMedia, posterInstance);
                }
                else
                {
                    Console.Error.WriteLine("RichMediaPoster type or Poster property not found – poster will not be set.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error while setting poster via reflection: {ex.Message}");
            }

            // Apply changes to the annotation
            richMedia.Update();

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich media annotation added and saved to '{outputPath}'.");
    }
}
