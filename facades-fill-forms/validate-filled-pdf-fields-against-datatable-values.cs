using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfFormComparison
{
    class Program
    {
        private const string TemplatePdfPath = "template_form.pdf";
        private const string FilledPdfPath   = "filled_form.pdf";

        // Creates a sample DataTable whose column names match the PDF form field names.
        private static DataTable CreateSampleData()
        {
            var table = new DataTable();
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Email", typeof(string));

            var row = table.NewRow();
            row["FirstName"] = "John";
            row["LastName"]  = "Doe";
            row["Email"]     = "john.doe@example.com";
            table.Rows.Add(row);

            return table;
        }

        static void Main()
        {
            // 1. Verify that the template PDF exists.
            if (!File.Exists(TemplatePdfPath))
            {
                Console.WriteLine($"FAIL: Template PDF not found at path: {TemplatePdfPath}");
                return;
            }

            // 2. Prepare source data.
            DataTable sourceData = CreateSampleData();
            DataRow   sourceRow  = sourceData.Rows[0];

            // 3. Fill the PDF form using Aspose.Pdf.Facades.Form.
            try
            {
                using (var doc = new Document(TemplatePdfPath))
                {
                    // The Form facade works with a Document instance.
                    using (var pdfForm = new Form(doc))
                    {
                        foreach (DataColumn col in sourceData.Columns)
                        {
                            string fieldName  = col.ColumnName;
                            string fieldValue = Convert.ToString(sourceRow[col]);
                            // Correct method to set a field value.
                            pdfForm.FillField(fieldName, fieldValue);
                        }
                    }
                    doc.Save(FilledPdfPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL: Exception while filling PDF – {ex.Message}");
                return;
            }

            // 4. Verify that each field in the filled PDF matches the original DataTable values.
            bool allMatch = true;
            try
            {
                using (var filledDoc = new Document(FilledPdfPath))
                {
                    var fields = filledDoc.Form?.Fields;
                    foreach (DataColumn col in sourceData.Columns)
                    {
                        string fieldName     = col.ColumnName;
                        string expectedValue = Convert.ToString(sourceRow[col]);

                        // Locate the field by its name (or FullName if Name is null).
                        var field = fields?.FirstOrDefault(f =>
                            string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                            string.Equals(f.FullName, fieldName, StringComparison.Ordinal));

                        string actualValue = field?.Value?.ToString() ?? string.Empty;

                        if (!string.Equals(expectedValue, actualValue))
                        {
                            allMatch = false;
                            Console.WriteLine($"FAIL: Field '{fieldName}' mismatch. Expected: '{expectedValue}', Actual: '{actualValue}'.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL: Exception while reading filled PDF – {ex.Message}");
                allMatch = false;
            }

            // 5. Report overall result.
            Console.WriteLine(allMatch ? "PASS: All form fields match the source data." : "FAIL: One or more fields did not match.");

            // 6. Cleanup generated file.
            try
            {
                if (File.Exists(FilledPdfPath))
                    File.Delete(FilledPdfPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"WARNING: Could not delete temporary file – {ex.Message}");
            }
        }
    }
}
