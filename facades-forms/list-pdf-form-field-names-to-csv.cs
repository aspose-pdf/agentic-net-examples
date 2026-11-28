using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "fieldslist.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document first, then bind it to the Form facade.
        // The Form facade provides the FieldNames property to enumerate form fields.
        using (Document doc = new Document(inputPdfPath))
        using (Form pdfForm = new Form(doc))
        {
            // Retrieve all field names from the form.
            // FieldNames returns a string[] containing the names of the form fields.
            string[] fieldNames = pdfForm.FieldNames ?? Array.Empty<string>();

            // Write the field names to a CSV file (one name per line).
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // Optional header row.
                writer.WriteLine("FieldName");

                foreach (string name in fieldNames)
                {
                    // Escape double quotes if they appear in the field name.
                    string escaped = name.Replace("\"", "\"\"");
                    writer.WriteLine($"\"{escaped}\"");
                }
            }

            Console.WriteLine($"Field list saved to '{outputCsvPath}'.");
        }
    }
}
