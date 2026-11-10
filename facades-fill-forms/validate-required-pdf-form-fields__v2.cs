using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfFormValidator
{
    // Returns true if every required form field in the PDF has a matching column in the DataTable.
    // Throws InvalidOperationException listing missing columns.
    public static bool ValidateRequiredFields(string pdfPath, DataTable dataTable)
    {
        if (!System.IO.File.Exists(pdfPath))
            throw new System.IO.FileNotFoundException($"PDF file not found: {pdfPath}");

        // Load the PDF document.
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document actually contains a form with fields.
            if (doc.Form == null || doc.Form.Fields == null || doc.Form.Fields.Count() == 0)
                throw new InvalidOperationException("PDF does not contain any form fields.");

            // Collect names of required fields that are not present as columns in the DataTable.
            List<string> missingColumns = new List<string>();

            foreach (var field in doc.Form.Fields)
            {
                // Some field types may have a null Name; fall back to FullName.
                string fieldName = field.Name ?? field.FullName;

                // The Required flag is exposed as a boolean property named 'Required'.
                if (field.Required && !dataTable.Columns.Contains(fieldName))
                {
                    missingColumns.Add(fieldName);
                }
            }

            // If any required fields are missing, report them.
            if (missingColumns.Count > 0)
            {
                string missing = string.Join(", ", missingColumns);
                throw new InvalidOperationException(
                    $"Missing DataTable columns for required PDF form fields: {missing}");
            }

            // All required fields have matching columns.
            return true;
        }
    }
}

class Program
{
    // Entry point required for a console application.
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: PdfFormValidator <pdfPath> <dataTableXmlPath>");
            return;
        }

        string pdfPath = args[0];
        string xmlPath = args[1];

        // Load a DataTable from an XML file (any source that populates the table is acceptable).
        DataTable dt = new DataTable();
        dt.ReadXml(xmlPath);

        try
        {
            bool result = PdfFormValidator.ValidateRequiredFields(pdfPath, dt);
            Console.WriteLine(result ? "All required fields are present." : "Validation failed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }
    }
}
