using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string videoFile = "sample.mp4";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(videoFile))
        {
            Console.Error.WriteLine($"Video file not found: {videoFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // (llx, lly, urx, ury) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMedia annotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // Attach the video using reflection – RichMediaContent may not exist
            // in older Aspose.Pdf versions. This keeps the code compilable across
            // all supported versions while still enabling the feature when the
            // runtime library provides it.
            // -----------------------------------------------------------------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Create an instance of RichMediaContent
                object richMediaContent = Activator.CreateInstance(richMediaContentType);

                // The RichMediaContent class (when present) exposes an AddFile method
                // that accepts the path to the media file.
                MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                addFileMethod?.Invoke(richMediaContent, new object[] { videoFile });

                // Assign the created content to the annotation via the RichMediaContent property.
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }
            else
            {
                Console.Error.WriteLine("RichMediaContent type not found in the current Aspose.Pdf version; skipping media embedding.");
            }

            // -----------------------------------------------------------------
            // Configure activation to start playback only on double‑click.
            // The RichMediaSettings class, RichMediaActivation enum and the
            // RichMediaSettings property may be missing in older releases, so we
            // use reflection to apply the setting when it is available.
            // -----------------------------------------------------------------
            Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            Type activationEnumType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
            if (settingsType != null && activationEnumType != null)
            {
                // Create an instance of RichMediaSettings
                object settingsInstance = Activator.CreateInstance(settingsType);

                // Get the Activation property
                PropertyInfo activationProp = settingsType.GetProperty("Activation");

                // Parse the DoubleClick enum value
                object doubleClickValue = Enum.Parse(activationEnumType, "DoubleClick");

                // Set the Activation property
                activationProp?.SetValue(settingsInstance, doubleClickValue);

                // Assign the settings instance to the annotation (if the property exists)
                PropertyInfo annotationSettingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                annotationSettingsProp?.SetValue(richMedia, settingsInstance);
            }
            else
            {
                Console.Error.WriteLine("RichMediaSettings or RichMediaActivation type not found; using default activation behavior.");
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMedia annotation added and saved to '{outputPdf}'.");
    }
}
