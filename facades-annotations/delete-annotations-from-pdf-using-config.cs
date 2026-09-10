using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string configFilePath = "annotations_to_delete.txt"; // one annotation type per line
        const string outputPdfPath  = "output.pdf";

        // Validate input files
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

        // Read annotation types from the configuration file.
        // Empty lines or whitespace are ignored.
        List<string> annotationTypes = File.ReadAllLines(configFilePath)
                                           .Where(line => !string.IsNullOrWhiteSpace(line))
                                           .Select(line => line.Trim())
                                           .ToList();

        // Use PdfAnnotationEditor to manipulate annotations.
        // The class implements IDisposable via SaveableFacade, so wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPdfPath);

            if (annotationTypes.Count == 0)
            {
                // No specific types provided – delete all annotations.
                editor.DeleteAnnotations();
            }
            else
            {
                // Delete annotations of each specified type.
                foreach (string annotType in annotationTypes)
                {
                    // The DeleteAnnotations(string) overload removes all annotations of the given type.
                    editor.DeleteAnnotations(annotType);
                }
            }

            // Save the modified PDF.
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations processed. Output saved to '{outputPdfPath}'.");
    }
}