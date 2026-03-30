using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
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
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Create a DataTable whose column names match the PDF form field names
        DataTable dataTable = new DataTable("Data");
        dataTable.Columns.Add("FirstName", typeof(string));
        dataTable.Columns.Add("LastName", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));

        for (int i = 0; i < 10; i++)
        {
            DataRow row = dataTable.NewRow();
            row["FirstName"] = "John" + i;
            row["LastName"] = "Doe" + i;
            row["Address"] = $"123{i} Main St";
            dataTable.Rows.Add(row);
        }

        // Fill the PDF template with the data table
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePath);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPath);
        autoFiller.Close();

        // Verify that the generated PDF does not exceed the size limit
        FileInfo fileInfo = new FileInfo(outputPath);
        long fileSize = fileInfo.Length;
        Console.WriteLine($"Generated PDF size: {fileSize} bytes");
        if (fileSize > maxSizeBytes)
        {
            Console.WriteLine("WARNING: PDF size exceeds the predefined limit.");
        }
        else
        {
            Console.WriteLine("PDF size is within the allowed limit.");
        }
    }
}