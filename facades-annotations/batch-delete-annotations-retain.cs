using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string configPath = "retain-config.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }

        // Load configuration JSON that lists annotation types to retain
        string json = File.ReadAllText(configPath);
        RetainConfig config = JsonSerializer.Deserialize<RetainConfig>(json);
        if (config == null || config.Retain == null)
        {
            Console.Error.WriteLine("Invalid configuration file.");
            return;
        }

        // Build a case‑insensitive set for fast lookup
        HashSet<string> retainSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (string type in config.Retain)
        {
            retainSet.Add(type);
        }

        // Open PDF with PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            Document doc = editor.Document; // underlying document
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                AnnotationCollection annColl = page.Annotations;

                // Collect indices of annotations that are NOT in the retain list
                List<int> indicesToDelete = new List<int>();
                for (int i = 1; i <= annColl.Count; i++)
                {
                    Annotation ann = annColl[i];
                    string annType = ann.GetType().Name; // e.g., TextAnnotation, HighlightAnnotation
                    if (annType.EndsWith("Annotation", StringComparison.Ordinal))
                    {
                        annType = annType.Substring(0, annType.Length - "Annotation".Length);
                    }
                    if (!retainSet.Contains(annType))
                    {
                        indicesToDelete.Add(i);
                    }
                }

                // Delete from highest index to lowest to keep collection indices valid
                for (int i = indicesToDelete.Count - 1; i >= 0; i--)
                {
                    annColl.Delete(indicesToDelete[i]);
                }
            }

            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations retained according to config. Saved to '{outputPath}'.");
    }

    private class RetainConfig
    {
        public List<string> Retain { get; set; }
    }
}