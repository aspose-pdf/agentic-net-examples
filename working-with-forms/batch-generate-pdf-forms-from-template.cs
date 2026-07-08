using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputDir = "GeneratedForms";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Simulated database records
        DataTable records = GetSampleData();

        int index = 1;
        foreach (DataRow row in records.Rows)
        {
            // Load a fresh copy of the template for each record
            using (Document doc = new Document(templatePath))
            {
                // Populate form fields; field names are assumed to match column names
                foreach (DataColumn col in records.Columns)
                {
                    string fieldName = col.ColumnName;
                    string value = row[col]?.ToString() ?? string.Empty;

                    // Ensure the field exists before assigning a value
                    if (doc.Form.HasField(fieldName))
                    {
                        // The Form indexer returns a WidgetAnnotation; cast it to a Field
                        Field? field = doc.Form[fieldName] as Field;
                        if (field != null)
                        {
                            field.Value = value; // works for text boxes, check boxes, etc.
                        }
                    }
                }

                string outputPath = Path.Combine(outputDir, $"Form_{index}.pdf");
                doc.Save(outputPath); // Save as PDF
                Console.WriteLine($"Saved: {outputPath}");
            }

            index++;
        }
    }

    // Helper method to create sample data; replace with real DB access as needed
    static DataTable GetSampleData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("FirstName");
        table.Columns.Add("LastName");
        table.Columns.Add("Email");

        table.Rows.Add("John", "Doe", "john.doe@example.com");
        table.Rows.Add("Jane", "Smith", "jane.smith@example.com");
        table.Rows.Add("Bob", "Johnson", "bob.johnson@example.com");

        return table;
    }
}