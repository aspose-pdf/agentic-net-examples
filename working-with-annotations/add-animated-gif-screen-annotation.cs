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
        const string outputPdfPath = "output.pdf";
        const string gifPath = "animation.gif";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"Animated GIF not found: {gifPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a page three.");
                return;
            }

            // Get page three (1‑based indexing)
            Page page = doc.Pages[3];

            // Define the annotation rectangle (lower‑left X/Y, upper‑right X/Y)
            Rectangle rect = new Rectangle(100, 500, 300, 700);

            // Create a RichMediaAnnotation (Screen annotation that can host media)
            // Use the constructor that is available in all supported versions.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // Attach the animated GIF if the RichMediaContent type is present.
            // This avoids compile‑time dependency on newer Aspose.Pdf versions.
            // ---------------------------------------------------------------------
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Create an instance of RichMediaContent
                object richMediaContent = Activator.CreateInstance(richMediaContentType);

                // The method name differs between versions – try AddAsset first, then AddFile.
                MethodInfo addMethod = richMediaContentType.GetMethod("AddAsset", new[] { typeof(string), typeof(string) }) ??
                                        richMediaContentType.GetMethod("AddFile", new[] { typeof(string) });
                if (addMethod != null)
                {
                    // Prefer the overload that also takes a MIME type when available.
                    ParameterInfo[] parameters = addMethod.GetParameters();
                    if (parameters.Length == 2)
                        addMethod.Invoke(richMediaContent, new object[] { gifPath, "image/gif" });
                    else
                        addMethod.Invoke(richMediaContent, new object[] { gifPath });
                }

                // Assign the content to the annotation via reflection.
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, richMediaContent);
            }

            // ---------------------------------------------------------------------
            // Configure activation (play on page open) and looping if RichMediaSettings exists.
            // ---------------------------------------------------------------------
            Type richMediaSettingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (richMediaSettingsType != null)
            {
                object settings = Activator.CreateInstance(richMediaSettingsType);

                // Activation – use RichMediaActivation with condition PageOpen
                Type activationType = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                Type activationConditionEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivationCondition, Aspose.Pdf");
                if (activationType != null && activationConditionEnum != null)
                {
                    object conditionValue = Enum.Parse(activationConditionEnum, "PageOpen");
                    object activation = Activator.CreateInstance(activationType, conditionValue);
                    PropertyInfo activationProp = richMediaSettingsType.GetProperty("Activation");
                    activationProp?.SetValue(settings, activation);
                }

                // PlayMode – set to Loop if the enum is available
                Type playModeEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaPlayMode, Aspose.Pdf");
                if (playModeEnum != null)
                {
                    object loopValue = Enum.Parse(playModeEnum, "Loop");
                    PropertyInfo playModeProp = richMediaSettingsType.GetProperty("PlayMode");
                    playModeProp?.SetValue(settings, loopValue);
                }

                // Assign the settings to the annotation via reflection.
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settings);
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Screen annotation with looping GIF added to page 3 and saved as '{outputPdfPath}'.");
    }
}
