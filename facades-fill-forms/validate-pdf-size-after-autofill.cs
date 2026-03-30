using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled.pdf";
        const long maxSizeBytes = 5 * 1024 * 1024; // 5 MB limit

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine("Template PDF not found: " + templatePath);
            return;
        }

        // Prepare a sample DataTable matching the fields in the template PDF
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));

        for (int i = 1; i <= 10; i++)
        {
            DataRow row = dataTable.NewRow();
            row["Name"] = "Customer " + i;
            row["Address"] = "Address " + i;
            dataTable.Rows.Add(row);
        }

        // Fill the template using AutoFiller
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);
        autoFiller.Close();

        // Verify that the generated PDF size does not exceed the predefined limit
        FileInfo fileInfo = new FileInfo(outputPath);
        long fileSize = fileInfo.Length;
        if (fileSize > maxSizeBytes)
        {
            Console.WriteLine("Warning: Generated PDF size (" + fileSize + " bytes) exceeds the limit of " + maxSizeBytes + " bytes.");
        }
        else
        {
            Console.WriteLine("Generated PDF size is within limit: " + fileSize + " bytes.");
        }
    }
}