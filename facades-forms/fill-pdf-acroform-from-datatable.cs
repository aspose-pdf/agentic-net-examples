using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Build a DataTable whose column names exactly match the PDF field PartialName values.
        DataTable formData = new DataTable();
        formData.Columns.Add("FirstName", typeof(string));
        formData.Columns.Add("LastName",  typeof(string));
        formData.Columns.Add("Email",     typeof(string));
        // AutoFiller expects string values; convert numbers to string beforehand.
        formData.Columns.Add("Age", typeof(string));

        DataRow row = formData.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Email"]     = "john.doe@example.com";
        row["Age"]       = "30"; // numeric values should be formatted as strings
        formData.Rows.Add(row);

        // AutoFiller workflow: BindPdf -> ImportDataTable -> Save -> Close.
        AutoFiller filler = new AutoFiller();

        // Bind the template PDF. The stream can be disposed after binding because AutoFiller copies the content.
        using (FileStream templateStream = File.OpenRead(templatePath))
        {
            filler.BindPdf(templateStream);
        }

        // Import the DataTable. Each column name must match a field name in the PDF.
        filler.ImportDataTable(formData);

        // Save the filled PDF to disk.
        filler.Save(outputPath);

        // Release any resources held by the filler.
        filler.Close();

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }
}
