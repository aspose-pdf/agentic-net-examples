using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple JSON string escaper
    private static string Escape(string s) => s?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? string.Empty;

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_clean.pdf";
        const string logPath    = "deletion_log.json";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the annotation editor on the loaded document
            PdfAnnotationEditor editor = new PdfAnnotationEditor(doc);

            // Open the log file for writing
            using (StreamWriter logWriter = new StreamWriter(logPath, false))
            {
                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    AnnotationCollection annotations = page.Annotations;

                    // Delete annotations while iterating backwards to keep indexes valid
                    for (int i = annotations.Count; i >= 1; i--)
                    {
                        Annotation ann = annotations[i];

                        // Gather required information
                        string name = ann.Name ?? string.Empty;
                        string type = ann.AnnotationType.ToString();

                        // Write a structured JSON line to the log
                        string logEntry = $"{{\"Name\":\"{Escape(name)}\",\"Type\":\"{Escape(type)}\",\"Page\":{pageNum}}}";
                        logWriter.WriteLine(logEntry);

                        // Delete the annotation by its index
                        annotations.Delete(i);
                    }
                }
            }

            // Save the modified PDF using the facade (lifecycle rule: use Save)
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Annotations deleted and logged to '{logPath}'. Clean PDF saved as '{outputPath}'.");
    }
}