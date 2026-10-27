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
        const string outputPath = "output_richmedia.pdf";
        const string videoUrl = "https://example.com/video.mp4";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Create a RichMediaAnnotation on the first page.
            // The rectangle defines the annotation's location on the page.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(doc.Pages[1], rect);

            // ---------------------------------------------------------------
            // 1. Attach streaming video if the Rich Media API is available.
            // ---------------------------------------------------------------
            // Types are looked up via reflection so the code compiles even when
            // the current Aspose.Pdf version does not contain the Rich Media API.
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            Type richMediaInstanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");

            if (richMediaContentType != null && richMediaInstanceType != null)
            {
                // Create an instance that points to the online video URL.
                object instance = Activator.CreateInstance(richMediaInstanceType, new object[] { new Uri(videoUrl) });

                // Create the container for the media content.
                object content = Activator.CreateInstance(richMediaContentType);

                // Add the instance to the content's Instances collection.
                PropertyInfo instancesProp = richMediaContentType.GetProperty("Instances");
                if (instancesProp != null)
                {
                    var instances = instancesProp.GetValue(content) as System.Collections.IList;
                    instances?.Add(instance);
                }

                // Assign the content to the annotation.
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, content);
            }
            else
            {
                // Rich Media API not available – the annotation will be added without media.
                Console.WriteLine("RichMediaContent or RichMediaInstance type not found. Adding annotation without media.");
            }

            // ---------------------------------------------------------------
            // 2. Configure Rich Media settings – disable download if supported.
            // ---------------------------------------------------------------
            Type richMediaSettingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            Type richMediaPermissionsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaPermissions, Aspose.Pdf");

            if (richMediaSettingsType != null && richMediaPermissionsType != null)
            {
                object settings = Activator.CreateInstance(richMediaSettingsType);

                // Set Permissions = NoDownload
                PropertyInfo permProp = richMediaSettingsType.GetProperty("Permissions");
                if (permProp != null)
                {
                    object noDownload = Enum.Parse(richMediaPermissionsType, "NoDownload");
                    permProp.SetValue(settings, noDownload);
                }

                // Assign the settings to the annotation.
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settings);
            }
            else
            {
                // Settings API not available – continue without explicit permissions.
                Console.WriteLine("RichMediaSettings or RichMediaPermissions type not found. Skipping permission configuration.");
            }

            // Add the annotation to the page's annotation collection.
            doc.Pages[1].Annotations.Add(richMedia);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich media PDF saved to '{outputPath}'.");
    }
}
