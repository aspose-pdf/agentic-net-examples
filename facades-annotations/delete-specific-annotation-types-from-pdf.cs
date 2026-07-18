using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string configPath = "annotationConfig.json";

        // Verify files exist
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

        // Read annotation types from JSON config (e.g., ["Text","Highlight","Line"])
        List<string> annotationTypes;
        try
        {
            string json = File.ReadAllText(configPath);
            annotationTypes = JsonSerializer.Deserialize<List<string>>(json);
            if (annotationTypes == null || annotationTypes.Count == 0)
            {
                Console.Error.WriteLine("Configuration file does not contain any annotation types.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Delete specified annotation types using PdfAnnotationEditor
        try
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(inputPdfPath);

                foreach (string annotType in annotationTypes)
                {
                    // Delete all annotations of the given type
                    editor.DeleteAnnotations(annotType);
                }

                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations deleted and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}