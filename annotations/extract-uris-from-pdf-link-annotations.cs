using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputJsonPath = "uris.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Collect all URIs from LinkAnnotations
        List<string> uris = new List<string>();

        // Document must be disposed via using (lifecycle rule)
        using (Document doc = new Document(inputPdfPath))
        {
            // Pages are 1‑based (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                foreach (Annotation annotation in page.Annotations)
                {
                    // Only process LinkAnnotation objects with a GoToURIAction
                    if (annotation is LinkAnnotation link && link.Action is GoToURIAction uriAction)
                    {
                        if (!string.IsNullOrEmpty(uriAction.URI))
                        {
                            uris.Add(uriAction.URI);
                        }
                    }
                }
            }
        }

        // Serialize the list of URIs to JSON and write to file
        string json = JsonSerializer.Serialize(uris, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputJsonPath, json);

        Console.WriteLine($"Extracted {uris.Count} URI(s) to '{outputJsonPath}'.");
    }
}
