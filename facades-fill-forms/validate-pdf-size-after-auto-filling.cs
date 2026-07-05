using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades; // Facade API for AutoFiller

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the resulting filled PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Maximum allowed file size (e.g., 5 MB)
        const long maxSizeBytes = 5L * 1024 * 1024;

        // Verify that the template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // ------------------------------------------------------------
        // Prepare a DataTable with data that matches the field names
        // in the PDF form (case‑sensitive). Replace this with real data
        // as needed.
        // ------------------------------------------------------------
        DataTable data = new DataTable("FormData");
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));

        // Example: populate 100 rows
        for (int i = 0; i < 100; i++)
        {
            data.Rows.Add($"Name {i}", $"Address {i}");
        }

        // ------------------------------------------------------------
        // Use AutoFiller to bind the template, import the DataTable,
        // and generate the merged PDF.
        // ------------------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);   // Input template PDF
            filler.ImportDataTable(data);   // Fill fields from DataTable
            filler.Save(outputPath);        // Output merged PDF (one file)
        }

        // ------------------------------------------------------------
        // Validate the size of the generated PDF against the limit.
        // ------------------------------------------------------------
        FileInfo resultInfo = new FileInfo(outputPath);
        if (resultInfo.Length > maxSizeBytes)
        {
            Console.Error.WriteLine(
                $"Generated PDF size {resultInfo.Length} bytes exceeds the limit of {maxSizeBytes} bytes.");
        }
        else
        {
            Console.WriteLine(
                $"PDF generated successfully. Size: {resultInfo.Length} bytes.");
        }
    }
}