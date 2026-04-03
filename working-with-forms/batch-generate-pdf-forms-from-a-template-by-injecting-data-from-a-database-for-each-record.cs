using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchFormFiller
{
    static void Main()
    {
        // Path to the PDF form template – resolved relative to the executable folder
        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormTemplate.pdf");

        // Verify that the template file exists before proceeding
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Error: Template file not found at '{templatePath}'. Ensure the file is copied to the output directory or provide a correct path.");
            return;
        }

        // Directory where generated PDFs will be saved
        const string outputDir = "GeneratedForms";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Simulated database data – replace with real DB access as needed
        DataTable data = GetSampleData();

        // Process each record and generate a filled PDF
        int recordIndex = 1;
        foreach (DataRow row in data.Rows)
        {
            // Load a fresh copy of the template for each record
            using (Document doc = new Document(templatePath))
            {
                // Fill each form field that matches a column name
                foreach (DataColumn col in data.Columns)
                {
                    string fieldName = col.ColumnName;
                    if (doc.Form.HasField(fieldName))
                    {
                        // Retrieve the field as a Field object (not just WidgetAnnotation)
                        Field field = doc.Form[fieldName] as Field;
                        if (field != null)
                        {
                            // Set the field value; convert to string to avoid type issues
                            field.Value = row[col]?.ToString() ?? string.Empty;
                        }
                    }
                }

                // Optional: flatten the form if you don't want editable fields in the output
                // doc.Form.Flatten();

                // Build output file name (e.g., GeneratedForms/Record_1.pdf)
                string outputPath = Path.Combine(outputDir, $"Record_{recordIndex}.pdf");

                // Save the filled PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Generated PDF for record {recordIndex}");
            recordIndex++;
        }
    }

    // Example method that creates a DataTable with sample data.
    // Replace this with actual database retrieval logic.
    static DataTable GetSampleData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("FirstName");
        table.Columns.Add("LastName");
        table.Columns.Add("Email");

        table.Rows.Add("Alice", "Smith", "alice.smith@example.com");
        table.Rows.Add("Bob", "Johnson", "bob.johnson@example.com");
        table.Rows.Add("Carol", "Williams", "carol.williams@example.com");

        return table;
    }
}
