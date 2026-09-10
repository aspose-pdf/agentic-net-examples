using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq; // Added for Count() extension method
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class FormDataComparer
{
    static void Main()
    {
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";
        const string reportPath = "form_diff_report.txt";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks (ensures proper disposal)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Dictionaries to hold field name -> value for each document
            var fields1 = ExtractFormFields(doc1);
            var fields2 = ExtractFormFields(doc2);

            // Build a diff report
            StringBuilder reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("Form Data Comparison Report");
            reportBuilder.AppendLine($"File 1: {Path.GetFileName(pdfPath1)}");
            reportBuilder.AppendLine($"File 2: {Path.GetFileName(pdfPath2)}");
            reportBuilder.AppendLine(new string('=', 50));
            reportBuilder.AppendLine();

            // Compare fields present in the first document
            foreach (var kvp in fields1)
            {
                string fieldName = kvp.Key;
                string value1 = kvp.Value;
                string value2 = fields2.ContainsKey(fieldName) ? fields2[fieldName] : "(missing)";

                if (value1 != value2)
                {
                    reportBuilder.AppendLine($"Field: {fieldName}");
                    reportBuilder.AppendLine($"  Value in {Path.GetFileName(pdfPath1)}: {value1}");
                    reportBuilder.AppendLine($"  Value in {Path.GetFileName(pdfPath2)}: {value2}");
                    reportBuilder.AppendLine();
                }
            }

            // Detect fields that exist only in the second document
            foreach (var kvp in fields2)
            {
                if (!fields1.ContainsKey(kvp.Key))
                {
                    reportBuilder.AppendLine($"Field: {kvp.Key}");
                    reportBuilder.AppendLine($"  Value in {Path.GetFileName(pdfPath1)}: (missing)");
                    reportBuilder.AppendLine($"  Value in {Path.GetFileName(pdfPath2)}: {kvp.Value}");
                    reportBuilder.AppendLine();
                }
            }

            // Write the report to a text file
            File.WriteAllText(reportPath, reportBuilder.ToString());
            Console.WriteLine($"Form data diff report saved to '{reportPath}'.");
        }
    }

    // Helper method to extract form field values into a dictionary
    private static Dictionary<string, string> ExtractFormFields(Document doc)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Ensure the document actually contains a form and fields collection
        if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Count() > 0) // Fixed Count usage
        {
            // The Fields collection is an IEnumerable of Field objects, not a dictionary.
            foreach (Field field in doc.Form.Fields)
            {
                // Guard against a null field reference (unlikely but defensive)
                if (field == null) continue;

                string fieldName = field.Name ?? string.Empty;
                // Some field types (e.g., CheckBoxField) expose the value via the "Value" property.
                // For safety, we convert the value to string, falling back to empty string if null.
                string fieldValue = field?.Value?.ToString() ?? string.Empty;
                result[fieldName] = fieldValue;
            }
        }

        return result;
    }
}
