using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfFormImporter
{
    /// <summary>
    /// Validates that every required field in the PDF form has a matching column in the supplied DataTable.
    /// Throws an exception if any required field is missing.
    /// </summary>
    /// <param name="pdfTemplatePath">Path to the PDF form template.</param>
    /// <param name="data">DataTable containing data to be imported.</param>
    public static void ValidateAndImport(string pdfTemplatePath, DataTable data)
    {
        if (!File.Exists(pdfTemplatePath))
            throw new FileNotFoundException($"Template PDF not found: {pdfTemplatePath}");

        // Bind the PDF form using the Form facade.
        using (Form form = new Form(pdfTemplatePath))
        {
            // Collect required field names that are missing in the DataTable.
            List<string> missingColumns = new List<string>();

            foreach (string fieldName in form.FieldNames)
            {
                // Check if the field is marked as required.
                if (form.IsRequiredField(fieldName))
                {
                    // Verify that the DataTable contains a column with the same name (case‑sensitive).
                    if (!data.Columns.Contains(fieldName))
                    {
                        missingColumns.Add(fieldName);
                    }
                }
            }

            // If any required fields are missing, abort the operation.
            if (missingColumns.Count > 0)
            {
                string missing = string.Join(", ", missingColumns);
                throw new InvalidOperationException(
                    $"The following required form fields are missing in the DataTable: {missing}");
            }

            // All required fields are present – proceed with the import.
            // Use the AutoFiller facade which maps column names to field names.
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the same PDF template to the AutoFiller.
                autoFiller.BindPdf(pdfTemplatePath);

                // Import the data. Column names must exactly match field names (case‑sensitive).
                autoFiller.ImportDataTable(data);

                // Save the filled PDF. The output path can be derived or passed as a parameter.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(pdfTemplatePath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(pdfTemplatePath) + "_filled.pdf");

                autoFiller.Save(outputPath);
                Console.WriteLine($"Form successfully filled and saved to '{outputPath}'.");
            }
        }
    }

    // Example usage.
    static void Main()
    {
        // Prepare a sample DataTable with columns that match the PDF form fields.
        DataTable table = new DataTable();
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Email", typeof(string));
        // Add a row of sample data.
        table.Rows.Add("John", "Doe", "john.doe@example.com");

        // Path to the PDF form template.
        const string templatePath = "template.pdf";

        try
        {
            ValidateAndImport(templatePath, table);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
