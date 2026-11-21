using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_richmedia.pdf";
        const string videoUrl = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates are in points; lower‑left to upper‑right)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation (core API – no Facades usage)
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // 1️⃣ Attach streaming video if the RichMediaContent API is available.
            // ---------------------------------------------------------------------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Create RichMediaContent instance
                object richMediaContent = Activator.CreateInstance(richMediaContentType);

                // Create FileSpecification for the online video (URL based)
                Type fileSpecType = Type.GetType("Aspose.Pdf.Annotations.FileSpecification, Aspose.Pdf");
                object fileSpec = null;
                if (fileSpecType != null)
                {
                    var ctor = fileSpecType.GetConstructor(new[] { typeof(string) });
                    if (ctor != null)
                        fileSpec = ctor.Invoke(new object[] { videoUrl });
                }

                // Create RichMediaInstance and link the FileSpecification
                Type richMediaInstanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                if (richMediaInstanceType != null && fileSpec != null)
                {
                    object instance = Activator.CreateInstance(richMediaInstanceType);
                    // Set File property
                    richMediaInstanceType.GetProperty("File")?.SetValue(instance, fileSpec);
                    // Set MIME type (helps the viewer)
                    richMediaInstanceType.GetProperty("MimeType")?.SetValue(instance, "video/mp4");

                    // Add the instance to RichMediaContent.Instances collection
                    var instancesProp = richMediaContentType.GetProperty("Instances");
                    var instances = instancesProp?.GetValue(richMediaContent) as System.Collections.IList;
                    instances?.Add(instance);
                }

                // Assign the prepared RichMediaContent to the annotation
                typeof(RichMediaAnnotation).GetProperty("RichMediaContent")?.SetValue(richMedia, richMediaContent);
            }

            // ---------------------------------------------------------------------
            // 2️⃣ Disable download option (if RichMediaSettings API exists).
            // ---------------------------------------------------------------------
            Type richMediaSettingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (richMediaSettingsType != null)
            {
                object settings = Activator.CreateInstance(richMediaSettingsType);

                // Permissions property expects a RichMediaPermissions enum value
                var permissionsProp = richMediaSettingsType.GetProperty("Permissions");
                if (permissionsProp != null)
                {
                    Type permEnumType = Type.GetType("Aspose.Pdf.Annotations.RichMediaPermissions, Aspose.Pdf");
                    if (permEnumType != null)
                    {
                        // Use the NoDownload flag to block downloading
                        object noDownload = Enum.Parse(permEnumType, "NoDownload");
                        permissionsProp.SetValue(settings, noDownload);
                    }
                }

                // Attach settings to the annotation
                typeof(RichMediaAnnotation).GetProperty("RichMediaSettings")?.SetValue(richMedia, settings);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia PDF saved to '{outputPath}'.");
    }
}
