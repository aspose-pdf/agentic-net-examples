using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportFormFields
{
    static void Main()
    {
        const string inputPdf = "FormB.pdf";
        const string outputFdf = "FormB_fields.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Enumerate all form fields and write basic info to console
            foreach (Field field in doc.Form)
            {
                string fieldName  = field.FullName ?? "(unnamed)";
                string fieldValue = field.Value?.ToString() ?? "(no value)";
                Console.WriteLine($"Field: {fieldName}, Type: {field.GetType().Name}, Value: {fieldValue}");
            }

            // Export the form data to an FDF file. The overload that takes only the file name
            // infers the required format from the ".fdf" extension, avoiding the missing enum value.
            doc.Save(outputFdf);
        }

        Console.WriteLine($"Form data exported to '{outputFdf}'.");
    }
}
