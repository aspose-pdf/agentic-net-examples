using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string targetPath = "target.pdf";
        const string embeddedPdfPath = "embed.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPath}");
            return;
        }
        if (!File.Exists(embeddedPdfPath))
        {
            Console.Error.WriteLine($"Embedded PDF not found: {embeddedPdfPath}");
            return;
        }

        // Load the PDF that will receive the RichMedia annotation
        using (Document doc = new Document(targetPath))
        {
            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMedia annotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // The RichMedia* types (RichMediaContent, RichMediaSettings, etc.)
            // are not available in older Aspose.Pdf versions. To keep the code
            // compilable across all versions we use reflection to create and
            // configure these objects only when they exist at runtime.
            // -----------------------------------------------------------------
            try
            {
                // RichMediaContent type
                Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                if (richMediaContentType != null)
                {
                    // Create RichMediaContent instance
                    object richMediaContent = Activator.CreateInstance(richMediaContentType);

                    // ----- Settings (ReadOnly + Activation) -----
                    Type settingsType = Type.GetType("Aspose.Pdf.Annotations.RichMediaSettings, Aspose.Pdf");
                    if (settingsType != null)
                    {
                        object settings = Activator.CreateInstance(settingsType);
                        // Set ReadOnly = true
                        var readOnlyProp = settingsType.GetProperty("ReadOnly");
                        readOnlyProp?.SetValue(settings, true);

                        // Set Activation = OnPageOpen (if enum exists)
                        Type activationEnum = Type.GetType("Aspose.Pdf.Annotations.RichMediaActivation, Aspose.Pdf");
                        if (activationEnum != null)
                        {
                            var onPageOpenValue = Enum.Parse(activationEnum, "OnPageOpen");
                            var activationProp = settingsType.GetProperty("Activation");
                            activationProp?.SetValue(settings, onPageOpenValue);
                        }

                        // Assign Settings to RichMediaContent
                        var settingsProp = richMediaContentType.GetProperty("Settings");
                        settingsProp?.SetValue(richMediaContent, settings);
                    }

                    // ----- Data (embed the external PDF) -----
                    Type dataType = Type.GetType("Aspose.Pdf.Annotations.RichMediaData, Aspose.Pdf");
                    if (dataType != null)
                    {
                        object data = Activator.CreateInstance(dataType);
                        var addFileMethod = dataType.GetMethod("AddFile", new[] { typeof(string) });
                        addFileMethod?.Invoke(data, new object[] { embeddedPdfPath });

                        // Assign Data to RichMediaContent
                        var dataProp = richMediaContentType.GetProperty("Data");
                        dataProp?.SetValue(richMediaContent, data);
                    }

                    // Finally attach the RichMediaContent to the annotation
                    var contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    contentProp?.SetValue(richMedia, richMediaContent);
                }
            }
            catch (Exception ex)
            {
                // If anything goes wrong we simply continue without the rich media payload.
                Console.Error.WriteLine($"RichMedia setup failed: {ex.Message}");
            }

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMedia PDF saved to '{outputPath}'.");
    }
}
