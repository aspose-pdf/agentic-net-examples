using System;
using System.IO;
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

        // Document disposal must be wrapped in a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (page-indexing-one-based rule)
            foreach (Page page in doc.Pages)
            {
                AnnotationCollection annotations = page.Annotations;

                // Annotation collections are also 1‑based
                for (int i = 1; i <= annotations.Count; i++)
                {
                    Annotation ann = annotations[i];

                    // Annotation type (enum)
                    string typeName = ann.AnnotationType.ToString();

                    // Rectangle – use fully qualified type to avoid ambiguity (fqtn-for-ambiguous-types rule)
                    Aspose.Pdf.Rectangle rect = ann.Rect;
                    string rectInfo = $"[{rect.LLX}, {rect.LLY}, {rect.URX}, {rect.URY}]";

                    // Media file name – only some annotation types carry a file reference.
                    // The FileSpecification property is not available in the current Aspose.Pdf version,
                    // so we default to "N/A". If a future version adds the property, the code can be
                    // extended to retrieve the file name via ann.FileSpecification?.Name.
                    string mediaFile = "N/A";

                    Console.WriteLine(
                        $"Page {page.Number}, Annotation {i}: Type={typeName}, Rect={rectInfo}, MediaFile={mediaFile}");
                }
            }
        }
    }
}
