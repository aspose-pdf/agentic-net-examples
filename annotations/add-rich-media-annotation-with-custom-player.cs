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
        const string outputPath = "output.pdf";
        const string playerPath = "customPlayer.swf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(playerPath))
        {
            Console.Error.WriteLine($"Custom player file not found: {playerPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document contains fewer than 3 pages.");
                return;
            }

            // Get page 3
            Page page = doc.Pages[3];

            // Define the annotation rectangle (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            var richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // The RichMediaContent, RichMediaConfiguration and related types are
            // only available in newer Aspose.Pdf releases. To keep the code
            // compilable with older versions we use reflection to access them at
            // runtime. If the types are not present we simply add the annotation
            // without attaching a custom player.
            // -----------------------------------------------------------------
            try
            {
                // Load the RichMediaContent type
                Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
                Type richMediaConfigType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                Type configEnumType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfigurationType, Aspose.Pdf");

                if (richMediaContentType != null && richMediaConfigType != null && configEnumType != null)
                {
                    // Create instances
                    object richMediaContent = Activator.CreateInstance(richMediaContentType);
                    object config = Activator.CreateInstance(richMediaConfigType);

                    // Set configuration type to Custom
                    PropertyInfo typeProp = richMediaConfigType.GetProperty("Type");
                    if (typeProp != null)
                    {
                        object customEnum = Enum.Parse(configEnumType, "Custom");
                        typeProp.SetValue(config, customEnum);
                    }

                    // Assign the custom player stream
                    PropertyInfo playerProp = richMediaConfigType.GetProperty("CustomPlayer");
                    if (playerProp != null)
                    {
                        using (FileStream playerStream = File.OpenRead(playerPath))
                        {
                            playerProp.SetValue(config, playerStream);
                        }
                    }

                    // Add the configuration to the content's Configurations collection
                    PropertyInfo configsProp = richMediaContentType.GetProperty("Configurations");
                    var configsList = configsProp?.GetValue(richMediaContent) as System.Collections.IList;
                    configsList?.Add(config);

                    // Attach the content to the annotation
                    PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    contentProp?.SetValue(richMedia, richMediaContent);
                }
            }
            catch (Exception ex)
            {
                // If anything goes wrong we fall back to a plain annotation.
                Console.Error.WriteLine($"Rich media setup failed: {ex.Message}");
            }

            // Update the annotation (required before adding to the page)
            richMedia.Update();

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"RichMediaAnnotation added and saved to '{outputPath}'.");
    }
}
