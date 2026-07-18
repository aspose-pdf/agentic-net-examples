using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Maximum allowed PDF size in bytes (example: 5 MB)
    private const long MaxPdfSizeBytes = 5L * 1024 * 1024;

    static void Main(string[] args)
    {
        // Expected arguments: templatePdfPath, outputPdfPath
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <templatePdfPath> <outputPdfPath>");
            return;
        }

        string templatePath = args[0];
        string outputPath   = args[1];

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        // Build a sample DataTable that matches the fields in the template PDF.
        DataTable data = CreateSampleDataTable();

        try
        {
            // AutoFiller handles binding, importing data and saving.
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF.
                filler.BindPdf(templatePath);

                // Import all rows from the DataTable.
                filler.ImportDataTable(data);

                // Save the merged result to the output file.
                filler.Save(outputPath);
            }

            // Verify the generated PDF size.
            ValidatePdfSize(outputPath, MaxPdfSizeBytes);

            Console.WriteLine($"PDF generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Creates a DataTable with column names that correspond to form field names.
    private static DataTable CreateSampleDataTable()
    {
        DataTable table = new DataTable("FormData");

        // Example field names – replace with actual field names from your PDF.
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName",  typeof(string));
        table.Columns.Add("Address",   typeof(string));
        table.Columns.Add("City",      typeof(string));
        table.Columns.Add("Country",   typeof(string));

        // Populate with sample rows.
        for (int i = 1; i <= 10; i++)
        {
            DataRow row = table.NewRow();
            row["FirstName"] = $"First{i}";
            row["LastName"]  = $"Last{i}";
            row["Address"]   = $"{i} Main St.";
            row["City"]      = "SampleCity";
            row["Country"]   = "SampleCountry";
            table.Rows.Add(row);
        }

        return table;
    }

    // Checks that the file size does not exceed the allowed limit.
    private static void ValidatePdfSize(string filePath, long maxSizeBytes)
    {
        FileInfo info = new FileInfo(filePath);
        if (!info.Exists)
            throw new FileNotFoundException("Generated PDF not found.", filePath);

        if (info.Length > maxSizeBytes)
        {
            // Optionally delete the oversized file.
            try { File.Delete(filePath); } catch { /* ignore */ }

            throw new InvalidOperationException(
                $"Generated PDF size ({info.Length} bytes) exceeds the limit of {maxSizeBytes} bytes.");
        }
    }
}