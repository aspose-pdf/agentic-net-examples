using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all form fields in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Identify sensitive fields by name (adjust as needed)
                string fieldName = field.FullName ?? field.Name ?? string.Empty;
                if (fieldName.Equals("ssn", StringComparison.OrdinalIgnoreCase) ||
                    fieldName.Equals("creditCard", StringComparison.OrdinalIgnoreCase))
                {
                    // Setting Exportable to false adds the NoExport flag,
                    // preventing the field from being included in exported data.
                    field.Exportable = false;
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with NoExport flags to '{outputPath}'.");
    }
}