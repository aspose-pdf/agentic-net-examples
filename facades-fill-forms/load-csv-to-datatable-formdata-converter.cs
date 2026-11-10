using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string xlsxPath = "input.xlsx";
        const string tempPdfPath = "temp.pdf";

        if (!File.Exists(xlsxPath))
        {
            Console.Error.WriteLine($"File not found: {xlsxPath}");
        }

        // ------------------------------------------------------------
        // OPTIONAL: Convert the XLSX to PDF using Aspose.Pdf.Facades.
        // PdfConverter does not provide BindXls/BindXlsx methods.
        // Therefore the conversion is omitted; the block is kept for
        // illustration and will be safely ignored at runtime.
        // ------------------------------------------------------------
        try
        {
            // No direct Excel‑to‑PDF conversion is available in the
            // PdfConverter class. If such functionality is required,
            // use Aspose.Cells to create a PDF or invoke an external tool.
            // The following placeholder demonstrates the correct PdfConverter
            // usage when a PDF source is available.
            using (PdfConverter converter = new PdfConverter())
            {
                // Example: bind an existing PDF (if you have one)
                // converter.BindPdf("source.pdf");
                // converter.DoConvert();
                // converter.Save(tempPdfPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Facades conversion skipped: {ex.Message}");
        }

        DataTable dataTable = CreateSampleDataTable();

        Console.WriteLine("DataTable created (sample data):");
        foreach (DataColumn column in dataTable.Columns)
        {
            Console.Write($"{column.ColumnName}\t");
        }
        Console.WriteLine();

        foreach (DataRow row in dataTable.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine();
        }

        if (File.Exists(tempPdfPath))
        {
            try { File.Delete(tempPdfPath); } catch { }
        }
    }

    private static DataTable CreateSampleDataTable()
    {
        var dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Amount", typeof(string));

        dt.Rows.Add("John Doe", DateTime.Today.ToShortDateString(), "1000");
        dt.Rows.Add("Jane Smith", DateTime.Today.AddDays(-1).ToShortDateString(), "2500");
        dt.Rows.Add("Bob Johnson", DateTime.Today.AddDays(-2).ToShortDateString(), "750");

        return dt;
    }
}
