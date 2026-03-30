using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF template (could be any source, here we read from file for demo)
        string templatePath = "template.pdf";
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Load the template PDF into a memory stream – no intermediate file is written
        byte[] pdfBytes = File.ReadAllBytes(templatePath);
        using (MemoryStream templateStream = new MemoryStream(pdfBytes))
        {
            // Prepare a DataTable whose column names match the form field names in the PDF
            DataTable dataTable = new DataTable("FormData");
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            dataTable.Columns.Add("Address", typeof(string));

            DataRow row = dataTable.NewRow();
            row["FirstName"] = "John";
            row["LastName"] = "Doe";
            row["Address"] = "123 Main St";
            dataTable.Rows.Add(row);

            // Use AutoFiller to bind the PDF stream and fill the fields
            using (AutoFiller autoFiller = new AutoFiller())
            {
                autoFiller.BindPdf(templateStream);
                autoFiller.ImportDataTable(dataTable);

                // Save the filled PDF into another memory stream (single merged stream)
                using (MemoryStream resultStream = new MemoryStream())
                {
                    autoFiller.Save(resultStream);
                    // Write the final PDF to disk – this is the only file operation
                    File.WriteAllBytes("filled_output.pdf", resultStream.ToArray());
                }
            }
        }

        Console.WriteLine("PDF filled and saved to filled_output.pdf");
    }
}