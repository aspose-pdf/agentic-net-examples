using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormA.pdf";
        const string outputPath = "FormA_fields.xfdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the form
        using (Document doc = new Document(inputPath))
        {
            // Enumerate all form fields and write their names and values to the console
            foreach (Field field in doc.Form)
            {
                // Some fields may not have a value; handle nulls safely
                string value = field.Value != null ? field.Value.ToString() : string.Empty;
                Console.WriteLine($"Field: {field.FullName}  Value: {value}");
            }

            // Export all form field data (as XFDF) to a file.
            // Document.ExportAnnotationsToXfdf writes the XFDF representation of the form fields/annotations.
            using (FileStream xfdfStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"Form data exported to '{outputPath}'.");
    }
}
