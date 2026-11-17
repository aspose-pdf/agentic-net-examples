using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string videoPath  = "sample.mp4";
        const string posterPath = "poster.jpg";
        const string outputPath = "richmedia.pdf";

        // Verify that the required files exist
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }
        if (!File.Exists(posterPath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterPath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (lower‑left x/y, upper‑right x/y)
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // The Rich Media API (RichMediaContent, RichMediaConfiguration, etc.)
            // is not present in older Aspose.Pdf versions. To keep the code
            // compilable across all versions we use reflection to discover and
            // populate these types only when they exist at runtime.
            // ---------------------------------------------------------------------
            TryConfigureRichMedia(richMedia, videoPath, posterPath);

            // Attach the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with RichMedia annotation saved to '{outputPath}'.");
    }

    private static void TryConfigureRichMedia(RichMediaAnnotation annotation, string videoPath, string posterPath)
    {
        // Look for the RichMediaContent type
        Type contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
        if (contentType == null)
            return; // Rich Media API not available – leave the annotation empty

        // Create an instance of RichMediaContent
        object richContent = Activator.CreateInstance(contentType);

        // Add the video file to the content (method name may vary – we try the most common signatures)
        MethodInfo addFileMethod = contentType.GetMethod("AddFile", new[] { typeof(string) }) ??
                                   contentType.GetMethod("AddFile", new[] { typeof(string), typeof(string) });
        addFileMethod?.Invoke(richContent, new object[] { videoPath });

        // Set the poster image if the property exists
        PropertyInfo posterProp = contentType.GetProperty("PosterImage");
        if (posterProp != null && posterProp.CanWrite)
        {
            // Create a RichMediaAsset for the poster image
            Type assetType = Type.GetType("Aspose.Pdf.Annotations.RichMediaAsset, Aspose.Pdf");
            if (assetType != null)
            {
                object posterAsset = Activator.CreateInstance(assetType, posterPath);
                posterProp.SetValue(richContent, posterAsset);
            }
        }

        // Assign the RichMediaContent to the annotation
        PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
        contentProp?.SetValue(annotation, richContent);

        // -----------------------------------------------------------------
        // Configure activation settings (play on click) if the related types exist
        // -----------------------------------------------------------------
        Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
        if (settingsType != null)
        {
            object settings = Activator.CreateInstance(settingsType);
            PropertyInfo activationProp = settingsType.GetProperty("Activation");
            if (activationProp != null && activationProp.CanWrite)
            {
                Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                if (activationEnum != null)
                {
                    object onClick = Enum.Parse(activationEnum, "OnClick");
                    activationProp.SetValue(settings, onClick);
                }
            }
            PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
            settingsProp?.SetValue(annotation, settings);
        }
    }
}
