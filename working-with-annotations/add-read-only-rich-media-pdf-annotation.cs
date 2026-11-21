using System;
using System.IO;
using System.Collections;
using System.Reflection; // Added for PropertyInfo
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // PDF to which the annotation will be added
        const string embedPdfPath = "embedded.pdf";   // PDF that will be embedded as rich media
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(embedPdfPath))
        {
            Console.Error.WriteLine($"Embedded PDF not found: {embedPdfPath}");
            return;
        }

        // Load the target PDF inside a using block (document‑disposal‑with‑using rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Use 1‑based page indexing (page‑indexing‑one‑based rule)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            // Fully qualify Rectangle to avoid ambiguity (fqtn‑for‑ambiguous‑types rule)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMediaAnn = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // Attempt to embed the PDF as Rich Media using reflection. This avoids
            // compile‑time dependencies on RichMediaContent, RichMediaConfiguration,
            // RichMediaInstance, etc., which may not exist in older Aspose.Pdf versions.
            // ---------------------------------------------------------------------
            try
            {
                // RichMediaContent type
                Type contentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (contentType != null)
                {
                    object content = Activator.CreateInstance(contentType);

                    // RichMediaConfiguration type
                    Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                    if (configType != null)
                    {
                        // ctor(string name)
                        object config = Activator.CreateInstance(configType, "EmbeddedPdfConfig");

                        // RichMediaInstance type
                        Type instanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                        if (instanceType != null)
                        {
                            object instance = Activator.CreateInstance(instanceType);

                            // Set Subtype = RichMediaSubtype.PDF
                            PropertyInfo subtypeProp = instanceType.GetProperty("Subtype");
                            Type subtypeEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaSubtype, Aspose.Pdf");
                            if (subtypeProp != null && subtypeEnum != null)
                            {
                                object pdfEnum = Enum.Parse(subtypeEnum, "PDF");
                                subtypeProp.SetValue(instance, pdfEnum);
                            }

                            // Create and assign Asset (RichMediaAsset)
                            Type assetType = Type.GetType("Aspose.Pdf.Annotations.RichMediaAsset, Aspose.Pdf");
                            if (assetType != null)
                            {
                                object asset = Activator.CreateInstance(assetType, "embedded.pdf", File.ReadAllBytes(embedPdfPath));
                                PropertyInfo assetProp = instanceType.GetProperty("Asset");
                                assetProp?.SetValue(instance, asset);
                            }

                            // Add instance to config.Instances (IList)
                            PropertyInfo instancesProp = configType.GetProperty("Instances");
                            if (instancesProp != null)
                            {
                                if (instancesProp.GetValue(config) is IList list)
                                    list.Add(instance);
                            }
                        }

                        // Add configuration to content.Configurations (IList)
                        PropertyInfo configsProp = contentType.GetProperty("Configurations");
                        if (configsProp != null)
                        {
                            if (configsProp.GetValue(content) is IList cfgList)
                                cfgList.Add(config);
                        }
                    }

                    // Assign the built content to the annotation (property may be missing in some versions)
                    PropertyInfo richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    richMediaContentProp?.SetValue(richMediaAnn, content);
                }
            }
            catch (Exception ex)
            {
                // If anything goes wrong, we simply continue without embedded media.
                Console.Error.WriteLine($"Rich media embedding failed: {ex.Message}");
            }

            // Disable all interactive features – make the annotation read‑only
            // 1) Set the annotation flag to ReadOnly
            richMediaAnn.Flags = AnnotationFlags.ReadOnly;

            // 2) If RichMediaSettings exists, set its ReadOnly property to true
            try
            {
                PropertyInfo settingsProp = typeof(RichMediaAnnotation).GetProperty("RichMediaSettings");
                if (settingsProp != null)
                {
                    object settings = settingsProp.GetValue(richMediaAnn);
                    if (settings == null)
                    {
                        Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
                        if (settingsType != null)
                        {
                            settings = Activator.CreateInstance(settingsType);
                            settingsProp.SetValue(richMediaAnn, settings);
                        }
                    }
                    if (settings != null)
                    {
                        PropertyInfo readOnlyProp = settings.GetType().GetProperty("ReadOnly");
                        readOnlyProp?.SetValue(settings, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Setting RichMediaSettings.ReadOnly failed: {ex.Message}");
            }

            // Add the annotation to the page
            page.Annotations.Add(richMediaAnn);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMedia annotation added. Output saved to '{outputPdfPath}'.");
    }
}
