using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the generated output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Maximum allowed file size in bytes (example: 5 MB)
        const long maxSizeBytes = 5L * 1024 * 1024;

        // ------------------------------------------------------------
        // Build a DataTable whose column names match the field names
        // in the PDF template (case‑sensitive).
        // ------------------------------------------------------------
        DataTable data = new DataTable();
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));

        // Populate the table with sample rows.
        for (int i = 0; i < 10; i++)
        {
            DataRow row = data.NewRow();
            row["Name"]    = $"Customer {i + 1}";
            row["Address"] = $"Address {i + 1}";
            data.Rows.Add(row);
        }

        // ------------------------------------------------------------
        // Use AutoFiller (Aspose.Pdf.Facades) to merge the data into
        // the PDF template and save the result.
        // ------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Error: Template file '{templatePath}' not found.");
            return;
        }

        using (AutoFiller filler = new AutoFiller())
        {
            // The InputFileName property is obsolete – use BindPdf instead.
            filler.BindPdf(templatePath);
            filler.ImportDataTable(data);
            filler.Save(outputPath);
        }

        // ------------------------------------------------------------
        // Validate the size of the generated PDF.
        // ------------------------------------------------------------
        FileInfo resultInfo = new FileInfo(outputPath);
        if (!resultInfo.Exists)
        {
            Console.Error.WriteLine($"Error: Generated PDF '{outputPath}' was not created.");
            return;
        }

        if (resultInfo.Length > maxSizeBytes)
        {
            Console.Error.WriteLine(
                $"Error: Generated PDF size {resultInfo.Length} bytes exceeds the limit of {maxSizeBytes} bytes.");
        }
        else
        {
            Console.WriteLine(
                $"PDF generated successfully. Size: {resultInfo.Length} bytes (within limit).");
        }
    }
}
