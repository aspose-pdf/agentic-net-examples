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
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        DataTable dataTable = CreateSampleDataTable();
        // Expect a column named "FileName" that will be used for the output PDF name.
        if (!dataTable.Columns.Contains("FileName"))
        {
            Console.Error.WriteLine("DataTable must contain a 'FileName' column for custom naming.");
            return;
        }

        foreach (DataRow row in dataTable.Rows)
        {
            // Load a fresh copy of the template for each row.
            using (Document pdfDoc = new Document(templatePath))
            {
                // Fill form fields whose names match column names.
                foreach (Field field in pdfDoc.Form)
                {
                    string fieldName = field.FullName;
                    if (dataTable.Columns.Contains(fieldName) && row[fieldName] != DBNull.Value)
                    {
                        field.Value = row[fieldName].ToString();
                    }
                }

                // Build the output file name from the "FileName" column value.
                string outputFileName = row["FileName"].ToString() + ".pdf";
                pdfDoc.Save(outputFileName);
                Console.WriteLine($"Saved: {outputFileName}");
            }
        }
    }

    // Creates a sample DataTable with fields matching the PDF form and a custom file name column.
    private static DataTable CreateSampleDataTable()
    {
        DataTable table = new DataTable("SampleData");
        // Add columns that correspond to form field names.
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Address", typeof(string));
        // Column used for custom output file naming.
        table.Columns.Add("FileName", typeof(string));

        // Add sample rows.
        DataRow row1 = table.NewRow();
        row1["FirstName"] = "John";
        row1["LastName"] = "Doe";
        row1["Address"] = "123 Main St";
        row1["FileName"] = "John_Doe_Invoice";
        table.Rows.Add(row1);

        DataRow row2 = table.NewRow();
        row2["FirstName"] = "Jane";
        row2["LastName"] = "Smith";
        row2["Address"] = "456 Oak Ave";
        row2["FileName"] = "Jane_Smith_Invoice";
        table.Rows.Add(row2);

        return table;
    }
}