using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormB.pdf";
        const string outputXfdf = "FormB_fields.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (var doc = new Document(inputPdf))
        {
            // Enumerate form fields (optional: you can iterate if needed)
            foreach (var field in doc.Form.Fields)
            {
                Console.WriteLine($"Field Name: {field.FullName}, Type: {field.GetType().Name}");
            }

            // Export all form field data (widget annotations) to XFDF using a stream
            using (var xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Form field data exported to '{outputXfdf}'.");
    }
}
