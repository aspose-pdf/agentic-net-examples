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
        const string inputPath = "input.pdf";
        const string outputJsonPath = "stamps.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Collect all stamp texts (or icon names) from the PDF
        List<string> stampTexts = new List<string>();

        // Document must be wrapped in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate over all annotations on the page
                foreach (Annotation annotation in page.Annotations)
                {
                    // StampAnnotation represents both text and image stamps
                    if (annotation is StampAnnotation stamp)
                    {
                        // The StampIcon enum does not contain a "None" member.
                        // For built‑in stamp types the enum value is defined, for custom
                        // image stamps the value is undefined (usually 0). We therefore
                        // add the icon name only when the enum value is a defined member.
                        if (Enum.IsDefined(typeof(StampIcon), stamp.Icon))
                        {
                            stampTexts.Add(stamp.Icon.ToString());
                        }
                    }
                }
            }
        }

        // Serialize the list of strings to a JSON array
        string json = JsonSerializer.Serialize(stampTexts, new JsonSerializerOptions { WriteIndented = true });

        // Write JSON to file
        File.WriteAllText(outputJsonPath, json);
        Console.WriteLine($"Extracted {stampTexts.Count} stamp text(s) to '{outputJsonPath}'.");
    }
}
