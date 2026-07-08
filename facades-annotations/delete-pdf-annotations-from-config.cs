using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

namespace PdfAnnotationDeletion
{
    // Model representing the JSON configuration file.
    public class AnnotationConfig
    {
        public List<string> DeleteTypes { get; set; } = new List<string>();
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath   = "input.pdf";
            const string outputPdfPath  = "output.pdf";
            const string configFilePath = "annotConfig.json";

            // Verify that the input PDF exists.
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            // Verify that the configuration file exists.
            if (!File.Exists(configFilePath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
                return;
            }

            // Read and deserialize the configuration file.
            AnnotationConfig config;
            try
            {
                string json = File.ReadAllText(configFilePath);
                config = JsonSerializer.Deserialize<AnnotationConfig>(json) ?? new AnnotationConfig();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // If no types are specified, nothing to delete.
            if (config.DeleteTypes == null || config.DeleteTypes.Count == 0)
            {
                Console.WriteLine("No annotation types specified for deletion.");
                return;
            }

            // Use PdfAnnotationEditor to delete the specified annotation types.
            try
            {
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    // Bind the source PDF.
                    editor.BindPdf(inputPdfPath);

                    // Delete each annotation type listed in the config.
                    foreach (string annotType in config.DeleteTypes)
                    {
                        // Guard against empty or whitespace entries.
                        if (string.IsNullOrWhiteSpace(annotType))
                            continue;

                        editor.DeleteAnnotations(annotType);
                        Console.WriteLine($"Deleted annotations of type: {annotType}");
                    }

                    // Save the modified PDF.
                    editor.Save(outputPdfPath);
                }

                Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
            }
        }
    }
}