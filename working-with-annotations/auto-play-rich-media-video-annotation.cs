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
        const string mediaPath = "sample.mp4"; // Path to the media file (e.g., video)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(mediaPath))
        {
            Console.Error.WriteLine($"Media file not found: {mediaPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the annotation will be placed (first page)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top) in points
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation (available in all supported versions)
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // 1. Attach media using reflection – works only if the RichMedia* types
            //    exist in the loaded Aspose.Pdf assembly. This avoids compile‑time
            //    errors on older versions where these classes are absent.
            // ---------------------------------------------------------------------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            Type richMediaConfigType   = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");

            if (richMediaContentType != null && richMediaConfigType != null)
            {
                // Create RichMediaConfiguration instance (ctor that accepts a file path)
                object configInstance = Activator.CreateInstance(richMediaConfigType, mediaPath);

                // Create RichMediaContent instance and add the configuration to it
                object contentInstance = Activator.CreateInstance(richMediaContentType);
                MethodInfo addConfigMethod = richMediaContentType.GetMethod("AddConfiguration", new[] { richMediaConfigType });
                addConfigMethod?.Invoke(contentInstance, new[] { configInstance });

                // Assign the content to the annotation via the RichMediaContent property
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, contentInstance);
            }
            else
            {
                // If the types are missing, we simply skip media attachment – the PDF
                // will still contain a valid RichMediaAnnotation object.
                Console.WriteLine("RichMediaContent/Configuration types not found – media will not be embedded.");
            }

            // ---------------------------------------------------------------------
            // 2. Set activation to play automatically when the page becomes visible.
            //    Use reflection for the same compatibility reason.
            // ---------------------------------------------------------------------
            Type activationType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
            Type activationModeEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivationMode, Aspose.Pdf");

            if (activationType != null && activationModeEnum != null)
            {
                // Parse the enum value "PageVisible"
                object modeValue = Enum.Parse(activationModeEnum, "PageVisible");
                // Create the activation object (ctor that accepts the enum value)
                object activationInstance = Activator.CreateInstance(activationType, modeValue);
                // Assign to the annotation
                PropertyInfo activationProp = typeof(RichMediaAnnotation).GetProperty("Activation");
                activationProp?.SetValue(richMedia, activationInstance);
            }
            else
            {
                Console.WriteLine("RichMediaActivation types not found – default activation will be used.");
            }

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia PDF saved to '{outputPath}'.");
    }
}
