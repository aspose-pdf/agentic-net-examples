using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const int pageNumber = 1; // 1‑based page index to inspect

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields present in the document.");
                return;
            }

            Console.WriteLine($"Form fields on page {pageNumber}:");
            // Enumerate all widget annotations (form fields) in the document
            foreach (WidgetAnnotation widget in form)
            {
                // Filter fields that belong to the requested page
                if (widget.PageIndex == pageNumber)
                {
                    string name = widget.Name ?? "(unnamed)";
                    string type = widget.GetType().Name;
                    string value = ExtractFieldValue(widget);
                    Console.WriteLine($"Name: {name}, Type: {type}, Value: {value}");
                }
            }
        }
    }

    // Helper to obtain the value of a field, handling both Form.Field and generic widgets
    static string ExtractFieldValue(WidgetAnnotation widget)
    {
        if (widget is Field field)
        {
            // Most form fields expose a Value property
            return field.Value?.ToString() ?? "(null)";
        }

        // Fallback for widgets that are not Field instances
        return widget.Contents ?? "(null)";
    }
}