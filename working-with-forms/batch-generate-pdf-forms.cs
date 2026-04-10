using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class PdfFormBatchGenerator
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

        // Retrieve data from a database (mocked here as a DataTable)
        DataTable records = GetDataFromDatabase();

        int recordIndex = 1;
        foreach (DataRow row in records.Rows)
        {
            // Load the PDF template for each record
            using (Document doc = new Document(templatePath))
            {
                // Fill each form field with the corresponding column value
                foreach (DataColumn col in records.Columns)
                {
                    string fieldName = col.ColumnName;
                    string fieldValue = row[col]?.ToString() ?? string.Empty;

                    // Ensure the field exists before assigning a value
                    if (doc.Form.HasField(fieldName))
                    {
                        // The indexer returns a Field (or a WidgetAnnotation in older versions).
                        // Cast to Field and use its Value property.
                        if (doc.Form[fieldName] is Field field)
                        {
                            field.Value = fieldValue;
                        }
                    }
                }

                // Save the populated PDF
                string outputPath = Path.Combine(outputDir, $"Form_{recordIndex}.pdf");
                doc.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }

            recordIndex++;
        }
    }

    // Mock method to simulate database retrieval; replace with real DB access logic
    static DataTable GetDataFromDatabase()
    {
        DataTable table = new DataTable();
        table.Columns.Add("FirstName");
        table.Columns.Add("LastName");
        table.Columns.Add("Email");

        // Example rows
        table.Rows.Add("John", "Doe", "john.doe@example.com");
        table.Rows.Add("Jane", "Smith", "jane.smith@example.com");

        return table;
    }
}
