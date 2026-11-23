using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string videoPath = "sample.mp4"; // path to the video file to embed
        const string outputPath = "output_with_video.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(videoPath))
        {
            Console.Error.WriteLine($"Video file not found: {videoPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page = doc.Pages[4];

            // Define the rectangle where the annotation will appear (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a RichMedia (video) annotation
            var videoAnnotation = new RichMediaAnnotation(page, rect);

            // Attach the video using reflection – works on both newer and older Aspose.Pdf versions
            var richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                var richMediaContent = Activator.CreateInstance(richMediaContentType);

                // Try the newer AddFile(string) method first
                var addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                if (addFileMethod != null)
                {
                    addFileMethod.Invoke(richMediaContent, new object[] { videoPath });
                }
                else
                {
                    // Fallback to older AddAsset(RichMediaAsset) pattern via reflection
                    var richMediaAssetType = Type.GetType("Aspose.Pdf.Annotations.RichMediaAsset, Aspose.Pdf");
                    if (richMediaAssetType != null)
                    {
                        var assetInstance = Activator.CreateInstance(richMediaAssetType, videoPath);
                        var addAssetMethod = richMediaContentType.GetMethod("AddAsset", new[] { richMediaAssetType });
                        addAssetMethod?.Invoke(richMediaContent, new[] { assetInstance });
                    }
                }

                // Assign the prepared content to the annotation (if the property exists)
                var contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(videoAnnotation, richMediaContent);
            }

            // Configure activation – play the video when the annotation is clicked (reflection‑based for compatibility)
            var settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (settingsType != null)
            {
                var settingsInstance = Activator.CreateInstance(settingsType);
                var activationProp = settingsType.GetProperty("Activation");
                var activationEnumType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                if (activationProp != null && activationEnumType != null)
                {
                    var onClickValue = Enum.Parse(activationEnumType, "OnClick");
                    activationProp.SetValue(settingsInstance, onClickValue);
                }

                var settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(videoAnnotation, settingsInstance);
            }

            // Add the annotation to the page
            page.Annotations.Add(videoAnnotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with video annotation: {outputPath}");
    }
}
