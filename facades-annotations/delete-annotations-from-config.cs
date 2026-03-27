using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string configFilePath = "annotations_to_delete.txt";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
            return;
        }

        // Read annotation types (one per line) from the configuration file
        string[] annotationTypes = File.ReadAllLines(configFilePath);

        // Use PdfAnnotationEditor to delete specified annotation types
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdfPath);

            foreach (string rawType in annotationTypes)
            {
                string type = rawType.Trim();
                if (type.Length == 0)
                {
                    continue; // skip empty lines
                }
                // Delete all annotations of this type
                editor.DeleteAnnotations(type);
            }

            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations deleted as per configuration. Output saved to '{outputPdfPath}'.");
    }
}