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
        const string outputPath = "filled.pdf";

        // Prepare a sample DataTable (in real scenarios this would come from a database)
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName");
        dataTable.Columns.Add("LastName");
        dataTable.Columns.Add("Email");
        DataRow row = dataTable.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Email"] = "john.doe@example.com";
        dataTable.Rows.Add(row);

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Load the PDF template
        using (Document doc = new Document(templatePath))
        {
            // Access the form fields
            using (Form pdfForm = new Form(doc))
            {
                // Collect required fields that are missing in the DataTable
                List<string> missingFields = new List<string>();
                foreach (string fieldName in pdfForm.FieldNames)
                {
                    if (pdfForm.IsRequiredField(fieldName) && !dataTable.Columns.Contains(fieldName))
                    {
                        missingFields.Add(fieldName);
                    }
                }

                if (missingFields.Count > 0)
                {
                    Console.Error.WriteLine("The following required fields are missing in the DataTable:");
                    foreach (string missing in missingFields)
                    {
                        Console.Error.WriteLine($"  - {missing}");
                    }
                    return; // Abort import because validation failed
                }

                // All required fields are present – import the data
                AutoFiller filler = new AutoFiller();
                filler.BindPdf(doc);
                filler.ImportDataTable(dataTable);
                doc.Save(outputPath);
                Console.WriteLine($"Form fields filled and saved to '{outputPath}'.");
            }
        }
    }
}