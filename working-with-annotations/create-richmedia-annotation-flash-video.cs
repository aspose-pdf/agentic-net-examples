using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string swfPath = "video.swf";
        const string outputPath = "richmedia_output.pdf";

        if (!File.Exists(swfPath))
        {
            Console.Error.WriteLine($"SWF file not found: {swfPath}");
            return;
        }

        // Create a new PDF document with a single page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the annotation rectangle (lower-left X, lower-left Y, upper-right X, upper-right Y).
            Rectangle rect = new Rectangle(100, 500, 400, 800);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // 1. Attach RichMediaContent (if the type exists in the loaded Aspose.Pdf).
            // ---------------------------------------------------------------------
            Type contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (contentType != null)
            {
                // Create RichMediaContent instance.
                object content = Activator.CreateInstance(contentType);

                // Load the SWF file and create a RichMediaAsset.
                using (FileStream swfStream = File.OpenRead(swfPath))
                {
                    Type assetType = Type.GetType("Aspose.Pdf.Annotations.RichMediaAsset, Aspose.Pdf");
                    object asset = Activator.CreateInstance(assetType, Path.GetFileName(swfPath), swfStream);

                    // content.Assets.Add(asset)
                    PropertyInfo assetsProp = contentType.GetProperty("Assets");
                    var assetsCollection = assetsProp.GetValue(content);
                    assetsCollection.GetType().GetMethod("Add").Invoke(assetsCollection, new[] { asset });

                    // Create configuration and instance linking to the asset.
                    Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                    object config = Activator.CreateInstance(configType);

                    Type instanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                    object instance = Activator.CreateInstance(instanceType);
                    instanceType.GetProperty("Asset")?.SetValue(instance, asset);
                    instanceType.GetProperty("MimeType")?.SetValue(instance, "application/x-shockwave-flash");

                    // config.Instances.Add(instance)
                    PropertyInfo instancesProp = configType.GetProperty("Instances");
                    var instancesColl = instancesProp.GetValue(config);
                    instancesColl.GetType().GetMethod("Add").Invoke(instancesColl, new[] { instance });

                    // content.Configurations.Add(config)
                    PropertyInfo configsProp = contentType.GetProperty("Configurations");
                    var configsColl = configsProp.GetValue(content);
                    configsColl.GetType().GetMethod("Add").Invoke(configsColl, new[] { config });
                }

                // Assign the prepared content to the annotation.
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                contentProp?.SetValue(richMedia, content);
            }
            else
            {
                Console.WriteLine("RichMediaContent type not available in this Aspose.Pdf version – media will not be embedded.");
            }

            // ---------------------------------------------------------------------
            // 2. Configure playback controls and activation behavior (if RichMediaSettings exists).
            // ---------------------------------------------------------------------
            Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
            if (settingsType != null)
            {
                object settings = Activator.CreateInstance(settingsType);

                // Activation = OnPageOpen
                Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                object activationVal = Enum.Parse(activationEnum, "OnPageOpen");
                settingsType.GetProperty("Activation")?.SetValue(settings, activationVal);

                // Deactivation = OnPageClose
                Type deactivationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaDeactivation, Aspose.Pdf");
                object deactivationVal = Enum.Parse(deactivationEnum, "OnPageClose");
                settingsType.GetProperty("Deactivation")?.SetValue(settings, deactivationVal);

                // Presentation with controls visible.
                Type presentationType = Type.GetType("Aspose.Pdf.Annotations.RichMediaPresentation, Aspose.Pdf");
                object presentation = Activator.CreateInstance(presentationType);
                presentationType.GetProperty("ShowControls")?.SetValue(presentation, true);
                settingsType.GetProperty("Presentation")?.SetValue(settings, presentation);

                // Assign settings to the annotation.
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                settingsProp?.SetValue(richMedia, settings);
            }

            // Add the annotation to the page and save.
            page.Annotations.Add(richMedia);
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia PDF saved to '{outputPath}'.");
    }
}
