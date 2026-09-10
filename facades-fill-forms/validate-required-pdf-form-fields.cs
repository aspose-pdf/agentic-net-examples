using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Example DataTable – in real usage this would come from a database or other source
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName",  typeof(string));
        dataTable.Columns.Add("Email",     typeof(string));
        // Add a sample row
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Email"]     = "john.doe@example.com";
        dataTable.Rows.Add(row);

        // Ensure the template PDF exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Load the PDF document inside a using block (document disposal rule)
        using (Document doc = new Document(templatePath))
        {
            // Initialize the Form facade on the loaded document
            using (Form form = new Form(doc))
            {
                // Collect required fields that are missing in the DataTable
                List<string> missingColumns = new List<string>();

                foreach (string fieldName in form.FieldNames)
                {
                    // Check if the field is marked as required
                    if (form.IsRequiredField(fieldName))
                    {
                        // Verify that the DataTable contains a column with the same name (case‑sensitive)
                        if (!dataTable.Columns.Contains(fieldName))
                        {
                            missingColumns.Add(fieldName);
                        }
                    }
                }

                // If any required fields are missing, abort the import
                if (missingColumns.Count > 0)
                {
                    Console.Error.WriteLine("The following required form fields have no matching DataTable columns:");
                    Console.Error.WriteLine(string.Join(", ", missingColumns));
                    return;
                }

                // Fill each field that has a matching column in the DataTable
                // (using the first data row for simplicity)
                foreach (DataColumn col in dataTable.Columns)
                {
                    // Only attempt to fill fields that actually exist in the PDF
                    if (Array.Exists(form.FieldNames, fn => fn == col.ColumnName))
                    {
                        // Convert the cell value to string; handle nulls safely
                        string value = dataTable.Rows[0][col] != DBNull.Value
                                       ? dataTable.Rows[0][col].ToString()
                                       : string.Empty;

                        // Fill the field – Form.FillField returns a bool indicating success
                        form.FillField(col.ColumnName, value);
                    }
                }

                // Save the filled PDF (save rule – use the facade's Save method)
                form.Save(outputPath);
                Console.WriteLine($"PDF form filled and saved to '{outputPath}'.");
            }
        }
    }
}