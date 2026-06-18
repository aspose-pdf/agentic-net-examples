using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF containing a required form field.
        using (Document templateDoc = new Document())
        {
            templateDoc.Pages.Add();
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextBoxField txtField = new TextBoxField(templateDoc.Pages[1], rect);
            txtField.PartialName = "Name";
            txtField.Required = true;
            templateDoc.Form.Add(txtField, 1);
            templateDoc.Save("template.pdf");
        }

        // Step 2: Prepare a DataTable that should supply values for the form.
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Age", typeof(int));
        DataRow dataRow = dataTable.NewRow();
        dataRow["Name"] = "John Doe";
        dataRow["Age"] = 30;
        dataTable.Rows.Add(dataRow);

        // Step 3: Validate that every required field has a matching column.
        using (Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form())
        {
            formFacade.BindPdf("template.pdf");
            System.Collections.Generic.IList<string> fieldNames = formFacade.FieldNames;
            foreach (string fieldName in fieldNames)
            {
                if (formFacade.IsRequiredField(fieldName))
                {
                    if (!dataTable.Columns.Contains(fieldName))
                    {
                        Console.WriteLine($"Required field '{fieldName}' is missing in the DataTable. Import aborted.");
                        return;
                    }
                }
            }

            // Step 4: All required fields are present – import the data using AutoFiller.
            using (Aspose.Pdf.Facades.AutoFiller autoFiller = new Aspose.Pdf.Facades.AutoFiller())
            {
                autoFiller.BindPdf("template.pdf");
                autoFiller.ImportDataTable(dataTable);
                autoFiller.Save("filled.pdf");
                Console.WriteLine("Form filled successfully.");
            }
        }
    }
}