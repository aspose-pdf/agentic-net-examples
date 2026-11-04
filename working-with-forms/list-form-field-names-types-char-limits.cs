using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder = "pdfs";
        // Path for the generated CSV report
        const string outputCsv = "form_fields_report.csv";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Create the CSV file and write the header line
        using (StreamWriter writer = new StreamWriter(outputCsv))
        {
            writer.WriteLine("FileName,FieldName,FieldType,CharacterLimit");

            // Process each PDF file in the folder
            foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
            {
                // Ensure deterministic disposal of the Document
                using (Document doc = new Document(pdfPath))
                {
                    // Skip files without a form
                    if (doc.Form == null || doc.Form.Fields == null)
                        continue;

                    // Iterate over all form fields in the document
                    foreach (Field field in doc.Form.Fields)
                    {
                        string fieldName   = field.FullName ?? field.PartialName ?? string.Empty;
                        string fieldType   = GetFieldType(field);
                        string charLimit   = GetCharacterLimit(field);
                        string csvLine     = $"{Path.GetFileName(pdfPath)},{Escape(fieldName)},{fieldType},{charLimit}";
                        writer.WriteLine(csvLine);
                    }
                }
            }
        }

        Console.WriteLine($"Report saved to {outputCsv}");
    }

    // Returns the concrete type name of the form field (e.g., TextBoxField, CheckboxField)
    static string GetFieldType(Field field)
    {
        return field.GetType().Name;
    }

    // Returns the character limit for text fields; otherwise "N/A"
    static string GetCharacterLimit(Field field)
    {
        if (field is TextBoxField txt)
        {
            // MaxLen == 0 means unlimited characters
            return txt.MaxLen > 0 ? txt.MaxLen.ToString() : "Unlimited";
        }
        return "N/A";
    }

    // Escapes commas and quotes in CSV fields
    static string Escape(string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (value.Contains(",") || value.Contains("\""))
        {
            string escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
        return value;
    }
}
