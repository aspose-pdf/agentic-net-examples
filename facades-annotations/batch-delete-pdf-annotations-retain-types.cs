using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

namespace AnnotationBatchDeletion
{
    // Represents the JSON configuration file.
    public class DeletionConfig
    {
        // List of annotation type names (e.g., "Text", "Highlight") to keep.
        public List<string> RetainAnnotationTypes { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string configPath = "config.json";   // JSON file defining retained types
            const string inputPdfPath = "input.pdf";   // Source PDF
            const string outputPdfPath = "output.pdf"; // Result PDF after deletion

            // Validate required files.
            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Load configuration.
            DeletionConfig config = JsonSerializer.Deserialize<DeletionConfig>(File.ReadAllText(configPath));
            // Use a case‑insensitive set for fast lookup.
            HashSet<string> retainSet = new HashSet<string>(config?.RetainAnnotationTypes ?? new List<string>(),
                                                            StringComparer.OrdinalIgnoreCase);

            // Use PdfAnnotationEditor (Facades API) to manipulate annotations.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Load the PDF document.
                editor.BindPdf(inputPdfPath);

                // Iterate over all possible annotation types defined in the enum.
                foreach (AnnotationType annotType in Enum.GetValues(typeof(AnnotationType)))
                {
                    string typeName = annotType.ToString();

                    // Delete the annotation type if it is NOT in the retain list.
                    if (!retainSet.Contains(typeName))
                    {
                        // DeleteAnnotations(string) removes all annotations of the specified type.
                        editor.DeleteAnnotations(typeName);
                    }
                }

                // Save the modified PDF.
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Batch deletion completed. Output saved to '{outputPdfPath}'.");
        }
    }
}