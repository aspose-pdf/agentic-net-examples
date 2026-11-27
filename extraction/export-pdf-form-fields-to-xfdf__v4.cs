using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormC.pdf";
        const string outputXfdf = "FormC_fields.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (var doc = new Document(inputPdf))
        {
            // Enumerate all form fields and write their names/types to the console
            Console.WriteLine("Form fields found in the document:");
            foreach (var field in doc.Form.Fields)
            {
                Console.WriteLine($"- Name: {field.FullName}, Type: {field.GetType().Name}");
            }

            // Export the form fields (and other annotations) to an XFDF file.
            // The ExportAnnotationsToXfdf method works with a Stream, not a file path.
            using (var xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Form data exported to XFDF file: {outputXfdf}");
    }
}
