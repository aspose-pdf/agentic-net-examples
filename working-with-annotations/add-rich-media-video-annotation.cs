using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_richmedia.pdf";
        const string videoPath = "sample.mp4";          // MP4 video to embed
        const string posterImagePath = "poster.jpg";   // Image shown before playback

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }
        if (!File.Exists(posterImagePath))
        {
            Console.Error.WriteLine($"Poster image not found: {posterImagePath}");
            return;
        }

        // Document disposal with using (hard rule)
        using (Document doc = new Document(inputPdfPath))
        {
            Page page = doc.Pages[1];
            // Rectangle: lower‑left (100,500) – upper‑right (400,800)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation (available in all supported versions)
            var richMedia = new RichMediaAnnotation(page, rect);

            // ---------- Embed video (reflection – works when RichMediaContent exists) ----------
            var richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
            if (richMediaContentProp != null)
            {
                // Try to obtain the RichMediaContent type from the Aspose.Pdf assembly
                var richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (richMediaContentType != null)
                {
                    // Create an instance of RichMediaContent
                    var richMediaContent = Activator.CreateInstance(richMediaContentType);
                    // The older API exposes an AddFile(string) method – use it if present
                    var addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                    if (addFileMethod != null)
                    {
                        addFileMethod.Invoke(richMediaContent, new object[] { videoPath });
                    }
                    // Assign the populated RichMediaContent to the annotation
                    richMediaContentProp.SetValue(richMedia, richMediaContent);
                }
            }

            // ---------- Set poster image (reflection – works when the property exists) ----------
            // First try a direct PosterImage property on the annotation
            var posterPropDirect = typeof(RichMediaAnnotation).GetProperty("PosterImage");
            if (posterPropDirect != null)
            {
                var posterSpec = new FileSpecification(posterImagePath);
                posterPropDirect.SetValue(richMedia, posterSpec);
            }
            else
            {
                // Fallback: navigate through RichMediaSettings if it exists
                var settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                if (settingsProp != null)
                {
                    var settingsInstance = settingsProp.GetValue(richMedia);
                    if (settingsInstance != null)
                    {
                        var posterProp = settingsInstance.GetType().GetProperty("PosterImage");
                        if (posterProp != null)
                        {
                            var posterSpec = new FileSpecification(posterImagePath);
                            posterProp.SetValue(settingsInstance, posterSpec);
                        }
                    }
                }
            }

            // Optional: activation behavior – uncomment if the runtime version supports it
            // var activationProp = typeof(RichMediaAnnotation).GetProperty("RichMediaActivation");
            // if (activationProp != null)
            //     activationProp.SetValue(richMedia, Enum.Parse(activationProp.PropertyType, "OnPageOpen"));

            // Add the annotation to the page and save
            page.Annotations.Add(richMedia);
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPdfPath}'.");
    }
}
