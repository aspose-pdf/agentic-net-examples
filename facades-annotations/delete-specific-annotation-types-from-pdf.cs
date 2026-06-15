using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string configFilePath = "annotations_to_delete.json";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Config file not found: {configFilePath}");
            return;
        }

        // Read configuration – expects a JSON array of annotation type strings, e.g. ["Text","Highlight"]
        string json = File.ReadAllText(configFilePath);
        string[] annotTypes = JsonSerializer.Deserialize<string[]>(json);

        // Use PdfAnnotationEditor facade to manipulate annotations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF
            editor.BindPdf(inputPdfPath);

            if (annotTypes != null && annotTypes.Length > 0)
            {
                // Delete only the specified annotation types
                foreach (string type in annotTypes)
                {
                    // The DeleteAnnotations(string) overload deletes all annotations of the given type
                    editor.DeleteAnnotations(type);
                }
            }
            else
            {
                // No specific types – delete all annotations
                editor.DeleteAnnotations();
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
            // Close releases the bound document (optional when using 'using')
            editor.Close();
        }

        Console.WriteLine($"Annotations processed. Output saved to '{outputPdfPath}'.");
    }
}