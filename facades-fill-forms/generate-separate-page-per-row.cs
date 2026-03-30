using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // <-- added

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string mergedOutput = "merged.pdf";

        // Ensure a simple template PDF exists – it contains form fields matching the DataTable columns.
        if (!File.Exists(templatePath))
        {
            CreateTemplatePdf(templatePath);
        }

        // Prepare a simple DataTable with two rows for demonstration.
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        DataRow row1 = dataTable.NewRow();
        row1["FirstName"] = "John";
        row1["LastName"] = "Doe";
        dataTable.Rows.Add(row1);
        DataRow row2 = dataTable.NewRow();
        row2["FirstName"] = "Jane";
        row2["LastName"] = "Smith";
        dataTable.Rows.Add(row2);

        // Use AutoFiller in *single‑document* mode to generate one PDF that contains a page per DataTable row.
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        // Do NOT set GeneratingPath, BasicFileName or OutputStreams – this keeps AutoFiller in single‑document mode.
        autoFiller.ImportDataTable(dataTable);
        // Save the combined document directly.
        autoFiller.Save(mergedOutput);

        // Verify that each DataTable row resulted in a separate page.
        using (Document mergedDoc = new Document(mergedOutput))
        {
            if (mergedDoc.Pages.Count == dataTable.Rows.Count)
            {
                Console.WriteLine($"SUCCESS: {mergedDoc.Pages.Count} pages match {dataTable.Rows.Count} data rows.");
            }
            else
            {
                Console.WriteLine($"FAILURE: {mergedDoc.Pages.Count} pages do not match {dataTable.Rows.Count} data rows.");
            }
        }

        Console.WriteLine($"Merged PDF saved to '{mergedOutput}'.");
    }

    // Helper method to create a minimal PDF template with form fields.
    private static void CreateTemplatePdf(string path)
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a text field for FirstName
            TextBoxField firstNameField = new TextBoxField(page, new Rectangle(100, 700, 300, 720))
            {
                PartialName = "FirstName",
                Value = ""
            };
            doc.Form.Add(firstNameField);

            // Add a text field for LastName
            TextBoxField lastNameField = new TextBoxField(page, new Rectangle(100, 650, 300, 670))
            {
                PartialName = "LastName",
                Value = ""
            };
            doc.Form.Add(lastNameField);

            doc.Save(path);
        }
    }
}
