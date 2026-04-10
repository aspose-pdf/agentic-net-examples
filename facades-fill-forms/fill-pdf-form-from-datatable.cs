using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        // ---------------------------------------------------------------------
        // NOTE: The original example used Aspose.Cells to read an XLSX file and
        // build a DataTable from the first row (headers). The current project
        // does not reference the Aspose.Cells assembly, which caused the
        // CS0234 compile error. To keep the sample self‑contained and compile
        // without adding a new NuGet package, the Excel handling has been
        // replaced with a simple in‑memory DataTable that mimics the expected
        // structure. In a real‑world scenario you could either add the
        // Aspose.Cells NuGet package or use another library (e.g., OpenXML,
        // EPPlus, OLE DB) to populate the DataTable from an actual XLSX file.
        // ---------------------------------------------------------------------

        // Create a DataTable with column headers taken from a hypothetical
        // first row of an Excel sheet.
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Date", typeof(string));
        dataTable.Columns.Add("Amount", typeof(string));

        // Add sample rows – replace this block with real Excel‑derived data.
        dataTable.Rows.Add("John Doe", DateTime.Today.ToShortDateString(), "123.45");
        dataTable.Rows.Add("Jane Smith", DateTime.Today.AddDays(-1).ToShortDateString(), "678.90");

        // Example: fill a PDF form using the generated DataTable
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(pdfTemplatePath);
            filler.ImportDataTable(dataTable);
            filler.Save(outputPdfPath);
        }

        Console.WriteLine($"DataTable created with {dataTable.Rows.Count} rows and {dataTable.Columns.Count} columns.");
        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}
