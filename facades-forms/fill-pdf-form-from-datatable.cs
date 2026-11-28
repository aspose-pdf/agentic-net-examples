using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form_template.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Synthetic DataTable with some columns
        DataTable sourceTable = new DataTable("FormData");
        sourceTable.Columns.Add("FirstName", typeof(string));
        sourceTable.Columns.Add("LastName", typeof(string));
        sourceTable.Columns.Add("Email", typeof(string));
        sourceTable.Columns.Add("UnusedColumn", typeof(string));

        // Add a single row of data
        sourceTable.Rows.Add("John", "Doe", "john.doe@example.com", "IgnoreMe");

        // Load the PDF form using Aspose.Pdf.Document (AutoFiller is deprecated)
        using (Document pdf = new Document(inputPdf))
        {
            // Existing field names in the PDF form
            string[] fieldNames = pdf.Form?.Fields?.Select(f => f.FullName).ToArray() ?? Array.Empty<string>();

            // Build a DataTable containing only columns that match the PDF field names
            DataTable matchingTable = new DataTable("MatchingData");
            foreach (DataColumn col in sourceTable.Columns)
            {
                if (Array.IndexOf(fieldNames, col.ColumnName) >= 0)
                {
                    matchingTable.Columns.Add(col.ColumnName, col.DataType);
                }
            }

            // Copy rows for the matching columns
            foreach (DataRow srcRow in sourceTable.Rows)
            {
                DataRow newRow = matchingTable.NewRow();
                foreach (DataColumn col in matchingTable.Columns)
                {
                    newRow[col.ColumnName] = srcRow[col.ColumnName];
                }
                matchingTable.Rows.Add(newRow);
            }

            // Import the matching data into the form if any columns matched
            if (matchingTable.Columns.Count > 0 && matchingTable.Rows.Count > 0)
            {
                DataRow dataRow = matchingTable.Rows[0]; // we have only one row in this example
                foreach (DataColumn col in matchingTable.Columns)
                {
                    var field = pdf.Form?.Fields?.FirstOrDefault(f => f.FullName == col.ColumnName);
                    if (field != null)
                    {
                        // Most PDF form fields are TextBoxField; fallback to generic field for other types
                        if (field is TextBoxField txtField)
                        {
                            txtField.Value = dataRow[col].ToString();
                        }
                        else
                        {
                            field.Value = dataRow[col].ToString();
                        }
                    }
                }
            }

            // Save the filled PDF
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPdf}'.");
    }
}
