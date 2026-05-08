using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // PDF with AcroForm
        const string outputPdfPath = "filled.pdf";

        // Verify that the template PDF exists before attempting to open it.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: The PDF template file '{inputPdfPath}' was not found.");
            return; // Abort execution – nothing to fill.
        }

        // Assume dataTable is populated elsewhere.
        DataTable dataTable = GetDataTable();

        // Load the PDF form using Form facade inside a using block for deterministic disposal.
        using (Form pdfForm = new Form(inputPdfPath))
        {
            // Collect names of required fields that are missing in the DataTable.
            List<string> missingColumns = new List<string>();

            foreach (string fieldName in pdfForm.FieldNames)
            {
                // Check if the field is marked as required.
                if (pdfForm.IsRequiredField(fieldName))
                {
                    // Verify that the DataTable contains a column with the same name (case‑sensitive).
                    if (!dataTable.Columns.Contains(fieldName))
                    {
                        missingColumns.Add(fieldName);
                    }
                }
            }

            // If any required field does not have a matching column, abort the import.
            if (missingColumns.Count > 0)
            {
                Console.Error.WriteLine("The following required form fields are missing in the DataTable:");
                foreach (string name in missingColumns)
                {
                    Console.Error.WriteLine($"  - {name}");
                }
                return; // Stop processing.
            }

            // All required fields are present – proceed with the import.
            // Example: fill fields using Form.FillField for the first DataRow.
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                foreach (DataColumn col in dataTable.Columns)
                {
                    // Fill only if the field exists in the PDF (optional safety check).
                    if (pdfForm.FieldNames.Contains(col.ColumnName))
                    {
                        // Use the string overload; other overloads exist for different field types.
                        pdfForm.FillField(col.ColumnName, row[col].ToString());
                    }
                }
            }

            // Save the updated PDF.
            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }

    // Placeholder method to obtain a populated DataTable.
    static DataTable GetDataTable()
    {
        // In a real scenario, fill the DataTable from a database, CSV, etc.
        DataTable table = new DataTable();
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Email", typeof(string));

        // Example row.
        table.Rows.Add("John", "Doe", "john.doe@example.com");

        return table;
    }
}
