using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Sample DataTable – in practice this would be populated from a database or other source
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Email", typeof(string));
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Email"] = "john.doe@example.com";
        dataTable.Rows.Add(row);

        // Load the PDF and check required form fields
        using (Document doc = new Document(inputPdf))
        {
            Form pdfForm = new Form(doc);
            System.Collections.Generic.List<string> requiredFields = new System.Collections.Generic.List<string>();
            foreach (string fieldName in pdfForm.FieldNames)
            {
                if (pdfForm.IsRequiredField(fieldName))
                {
                    requiredFields.Add(fieldName);
                }
            }

            // Validate that each required field has a matching column in the DataTable
            foreach (string required in requiredFields)
            {
                if (!dataTable.Columns.Contains(required))
                {
                    Console.Error.WriteLine($"Missing column for required field: {required}");
                    return;
                }
            }

            // All required fields are present – import the data into the PDF form
            AutoFiller autoFiller = new AutoFiller();
            autoFiller.BindPdf(inputPdf);
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save(outputPdf);
        }

        Console.WriteLine($"Form fields filled and saved to '{outputPdf}'.");
    }
}