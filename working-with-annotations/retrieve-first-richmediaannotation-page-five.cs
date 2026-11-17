using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 5 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            Page page = doc.Pages[5];

            // LINQ query: filter annotations to RichMediaAnnotation and take the first one
            RichMediaAnnotation richMedia = page.Annotations
                .OfType<RichMediaAnnotation>()
                .FirstOrDefault();

            if (richMedia == null)
            {
                Console.WriteLine("No RichMediaAnnotation found on page 5.");
                return;
            }

            // Display selected properties of the annotation
            Console.WriteLine($"Name: {richMedia.Name}");
            // Use AnnotationType instead of the non‑existent Subtype property
            Console.WriteLine($"Annotation Type: {richMedia.AnnotationType}");

            // RichMediaContent may not be present in older Aspose.Pdf versions.
            // Access it via reflection to keep the code compilable across versions.
            PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
            if (contentProp != null)
            {
                object content = contentProp.GetValue(richMedia);
                if (content != null)
                {
                    // Try to read the "Type" property of the content
                    PropertyInfo typeProp = content.GetType().GetProperty("Type");
                    object typeValue = typeProp?.GetValue(content);
                    Console.WriteLine($"Content Type: {typeValue ?? "<unknown>"}");

                    // Try to read the asset name (if any)
                    PropertyInfo assetProp = content.GetType().GetProperty("Asset");
                    object asset = assetProp?.GetValue(content);
                    string assetName = "<none>";
                    if (asset != null)
                    {
                        PropertyInfo nameProp = asset.GetType().GetProperty("Name");
                        assetName = nameProp?.GetValue(asset) as string ?? "<none>";
                    }
                    Console.WriteLine($"Asset Name: {assetName}");
                }
                else
                {
                    Console.WriteLine("RichMediaContent is null.");
                }
            }
            else
            {
                Console.WriteLine("RichMediaContent property is not available in this Aspose.Pdf version.");
            }
        }
    }
}
