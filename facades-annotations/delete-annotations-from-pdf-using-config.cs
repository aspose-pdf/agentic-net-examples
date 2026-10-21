using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the output PDF and the annotation configuration file
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath    = "annotations.json";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Read the configuration file.
            //    Expected format (JSON array of annotation type names):
            //    ["Text", "Highlight", "Stamp"]
            // ------------------------------------------------------------
            string json = File.ReadAllText(configPath);
            string[]? typeNames = JsonSerializer.Deserialize<string[]>(json);

            if (typeNames == null || typeNames.Length == 0)
            {
                Console.WriteLine("Configuration file does not contain any annotation types. No changes made.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Convert the string names to the Aspose.Pdf.Annotations.AnnotationType enum.
            //    Invalid names are ignored.
            // ------------------------------------------------------------
            var annotationTypes = new List<AnnotationType>();
            foreach (string name in typeNames)
            {
                if (Enum.TryParse<AnnotationType>(name, ignoreCase: true, out var parsed))
                {
                    annotationTypes.Add(parsed);
                }
                else
                {
                    Console.WriteLine($"Warning: Unknown annotation type '{name}' – ignored.");
                }
            }

            // If no valid types were found, nothing to delete.
            if (annotationTypes.Count == 0)
            {
                Console.WriteLine("No valid annotation types specified. No changes made.");
                return;
            }

            // ------------------------------------------------------------
            // 3. Delete the specified annotations using the Document API.
            // ------------------------------------------------------------
            Document pdfDoc = new Document(inputPdfPath);

            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate backwards because we will be removing items.
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    var annotation = page.Annotations[i];
                    if (annotationTypes.Contains(annotation.AnnotationType))
                    {
                        page.Annotations.Delete(i);
                    }
                }
            }

            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Annotations deleted. Result saved to '{outputPdfPath}'.");
        }
        catch (JsonException je)
        {
            Console.Error.WriteLine($"Error parsing configuration file: {je.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
