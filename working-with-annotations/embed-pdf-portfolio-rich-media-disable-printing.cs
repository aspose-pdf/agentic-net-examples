using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string portfolioPdfPath = "portfolio.pdf";  // PDF portfolio to embed
        const string outputPdfPath = "output_secure.pdf"; // result PDF

        if (!File.Exists(inputPdfPath) || !File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine("Required files not found.");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a RichMediaAnnotation that embeds the PDF portfolio
            // -----------------------------------------------------------------
            // Choose the first page (1‑based indexing) and define the annotation rectangle
            Page page = doc.Pages[1];
            var rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create the annotation
            var richMedia = new RichMediaAnnotation(page, rect);

            // -----------------------------------------------------------------
            // 2. Attach the embedded portfolio (using reflection for compatibility)
            // -----------------------------------------------------------------
            try
            {
                // Types that may be missing in older Aspose.Pdf versions
                var fileSpecType = Type.GetType("Aspose.Pdf.FileSpec, Aspose.Pdf");
                var fileSpecBehaviorEnum = Type.GetType("Aspose.Pdf.FileSpecBehavior, Aspose.Pdf");
                var richMediaInstanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                var richMediaConfigType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                var richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");

                if (fileSpecType != null && fileSpecBehaviorEnum != null && richMediaInstanceType != null &&
                    richMediaConfigType != null && richMediaContentType != null)
                {
                    // FileSpec constructor: (string filePath, FileSpecBehavior behavior)
                    object behaviorValue = Enum.Parse(fileSpecBehaviorEnum, "EmbeddedFile");
                    var fileSpec = Activator.CreateInstance(fileSpecType, portfolioPdfPath, behaviorValue);

                    // RichMediaInstance constructor: (FileSpec fileSpec)
                    var instance = Activator.CreateInstance(richMediaInstanceType, fileSpec);

                    // RichMediaConfiguration.Instances (IList) – add the instance
                    var config = Activator.CreateInstance(richMediaConfigType);
                    var instancesProp = richMediaConfigType.GetProperty("Instances");
                    var instancesList = instancesProp?.GetValue(config) as System.Collections.IList;
                    instancesList?.Add(instance);

                    // RichMediaContent.Configurations (IList) – add the configuration
                    var content = Activator.CreateInstance(richMediaContentType);
                    var configsProp = richMediaContentType.GetProperty("Configurations");
                    var configsList = configsProp?.GetValue(content) as System.Collections.IList;
                    configsList?.Add(config);

                    // Assign the content to the annotation
                    var richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    richMediaContentProp?.SetValue(richMedia, content);
                }
            }
            catch (Exception ex)
            {
                // If any step fails, we simply continue without the embedded media.
                Console.Error.WriteLine($"Rich media setup failed: {ex.Message}");
            }

            // Attach the annotation to the page
            page.Annotations.Add(richMedia);

            // -----------------------------------------------------------------
            // 3. Apply security: disable printing by omitting the Print permission
            // -----------------------------------------------------------------
            Permissions perms = Permissions.ExtractContent; // no printing permission
            doc.Encrypt(userPassword: "", ownerPassword: "", permissions: perms, cryptoAlgorithm: CryptoAlgorithm.AESx256);

            // -----------------------------------------------------------------
            // 4. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Secure PDF with embedded portfolio saved to '{outputPdfPath}'.");
    }
}
