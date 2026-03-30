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

        // Prepare sample data matching the PDF form field names
        DataTable dataTable = new DataTable("MailMerge");
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));

        for (int i = 0; i < 10; i++)
        {
            DataRow row = dataTable.NewRow();
            row["Name"] = "Customer " + i;
            row["Address"] = "123 Main St, City " + i;
            dataTable.Rows.Add(row);
        }

        try
        {
            // Load the template PDF inside a using block
            using (Document templateDoc = new Document(templatePath))
            {
                AutoFiller autoFiller = new AutoFiller();
                autoFiller.BindPdf(templateDoc);
                autoFiller.ImportDataTable(dataTable);
                autoFiller.Save(outputPath);
            }

            // Validate the generated PDF size
            FileInfo resultInfo = new FileInfo(outputPath);
            if (resultInfo.Length > maxSizeBytes)
            {
                Console.WriteLine($"Warning: PDF size {resultInfo.Length} bytes exceeds the limit of {maxSizeBytes} bytes.");
            }
            else
            {
                Console.WriteLine($"PDF size {resultInfo.Length} bytes is within the allowed limit.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
