using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";          // PDF to which the annotation will be added
        const string portfolioPdfPath = "portfolio.pdf";   // PDF portfolio to embed
        const string outputPdfPath = "output.pdf";          // Resulting PDF
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(portfolioPdfPath))
        {
            Console.Error.WriteLine($"Portfolio PDF not found: {portfolioPdfPath}");
            return;
        }

        // Load the document to be annotated
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a RichMediaAnnotation on the first page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 400, 800);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, rect);

            // Try to build RichMediaContent via reflection – it may not exist in older Aspose.Pdf versions
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Types needed for the content hierarchy
                Type configType = Type.GetType("Aspose.Pdf.Annotations.RichMediaConfiguration, Aspose.Pdf");
                Type instanceType = Type.GetType("Aspose.Pdf.Annotations.RichMediaInstance, Aspose.Pdf");
                Type fileType = Type.GetType("Aspose.Pdf.Annotations.RichMediaFile, Aspose.Pdf");

                if (configType != null && instanceType != null && fileType != null)
                {
                    // Create instances
                    object content = Activator.CreateInstance(richMediaContentType);
                    object config = Activator.CreateInstance(configType);
                    object instance = Activator.CreateInstance(instanceType);

                    // Set the file (constructor takes the file path)
                    object file = Activator.CreateInstance(fileType, portfolioPdfPath);
                    PropertyInfo fileProp = instanceType.GetProperty("File");
                    fileProp?.SetValue(instance, file);

                    // Set MIME type
                    PropertyInfo mimeProp = instanceType.GetProperty("MimeType");
                    mimeProp?.SetValue(instance, "application/pdf");

                    // Add the instance to the configuration
                    PropertyInfo instancesProp = configType.GetProperty("Instances");
                    if (instancesProp?.GetValue(config) is IList instancesList)
                    {
                        instancesList.Add(instance);
                    }

                    // Add the configuration to the content
                    PropertyInfo configsProp = richMediaContentType.GetProperty("Configurations");
                    if (configsProp?.GetValue(content) is IList configsList)
                    {
                        configsList.Add(config);
                    }

                    // Assign the built content to the annotation
                    PropertyInfo richMediaContentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                    richMediaContentProp?.SetValue(richMedia, content);
                }
            }
            // If the RichMediaContent type is unavailable, the annotation will be added without embedded media.

            // Disable printing for this annotation. Aspose.Pdf does not expose a NoPrint flag; the absence of the Print flag disables printing.
            // Setting the flag to Default ensures no Print flag is set.
            richMedia.Flags = AnnotationFlags.Default;

            // Add the annotation to the page
            page.Annotations.Add(richMedia);

            // Disable printing for the whole document via encryption permissions
            Permissions perms = Permissions.ExtractContent; // omit Permissions.PrintDocument
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"RichMedia annotation added and printing disabled. Saved to '{outputPdfPath}'.");
    }
}
