using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for SubmitFormAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string jsonPath  = "field_urls.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        var urls = new List<string>();

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Only button fields can have an OnActivated action
                if (field is ButtonField button && button.OnActivated is SubmitFormAction submitAction)
                {
                    // The Url property type varies between Aspose.Pdf versions:
                    //   * Newer versions: string
                    //   * Older versions: FileSpecification
                    // To handle both safely, cast to object first and then pattern‑match.
                    object urlObj = submitAction.Url;
                    if (urlObj is string urlString)
                    {
                        if (!string.IsNullOrEmpty(urlString))
                            urls.Add(urlString);
                    }
                    else if (urlObj is FileSpecification fileSpec)
                    {
                        // For FileSpecification we use the Name property which holds the original URL or file name.
                        string extracted = fileSpec.Name;
                        if (!string.IsNullOrEmpty(extracted))
                            urls.Add(extracted);
                    }
                }
            }
        }

        // Serialize the list of URLs to a pretty‑printed JSON file
        string json = JsonSerializer.Serialize(urls, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Exported {urls.Count} URL(s) to '{jsonPath}'.");
    }
}
