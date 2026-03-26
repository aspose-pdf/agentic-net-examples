using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Linq;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "fields_report.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            using (Document pdfDocument = new Document(inputPdf))
            {
                // FormEditor is instantiated in case future modifications are needed.
                FormEditor formEditor = new FormEditor(pdfDocument);

                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    writer.WriteLine("FieldName,FieldType");

                    var form = pdfDocument.Form;
                    if (form != null && form.Fields != null && form.Fields.Count() > 0)
                    {
                        foreach (var field in form.Fields)
                        {
                            string name = field.Name;
                            string type = field.GetType().Name; // e.g., TextBoxField, CheckBoxField, etc.
                            writer.WriteLine($"{EscapeCsv(name)},{type}");
                        }
                    }
                }
            }

            Console.WriteLine($"Form field report written to '{outputCsv}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Simple CSV escaping for commas and quotes
    private static string EscapeCsv(string s)
    {
        if (s.Contains('"') || s.Contains(','))
        {
            s = s.Replace("\"", "\"\"");
            return $"\"{s}\"";
        }
        return s;
    }
}
