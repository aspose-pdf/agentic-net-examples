using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string templatePdf = "template.pdf";          // PDF form template
        const string outputPdf   = "Consolidated.pdf";      // Final merged PDF
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfMergeTemp");
        Directory.CreateDirectory(tempFolder);

        // Ensure a template PDF exists – create a minimal one if missing (for demo purposes)
        if (!File.Exists(templatePdf))
        {
            var doc = new Document();
            doc.Pages.Add(); // add a blank page
            doc.Save(templatePdf);
        }

        // Assume we have an array of DataTables, each containing data for one filled PDF
        DataTable[] dataTables = GetDataTables(); // Replace with actual data retrieval

        // List to hold paths of the individually filled PDFs
        List<string> filledPdfPaths = new List<string>();

        // Fill the template for each DataTable and save to a temporary file
        for (int i = 0; i < dataTables.Length; i++)
        {
            string tempPdfPath = Path.Combine(tempFolder, $"filled_{i}.pdf");

            // AutoFiller fills the template using the current DataTable
            AutoFiller filler = new AutoFiller();
            // Use the new BindPdf method instead of the obsolete InputFileName property
            filler.BindPdf(templatePdf);
            filler.ImportDataTable(dataTables[i]);       // Populate fields
            filler.Save(tempPdfPath);                    // Save filled PDF

            filledPdfPaths.Add(tempPdfPath);
        }

        // Merge all filled PDFs into a single document using PdfFileEditor (Facades API)
        PdfFileEditor pdfEditor = new PdfFileEditor();
        pdfEditor.Concatenate(filledPdfPaths.ToArray(), outputPdf);

        // Optional: clean up temporary files
        foreach (string file in filledPdfPaths)
        {
            try { File.Delete(file); } catch { /* ignore cleanup errors */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Merged PDF created at: {outputPdf}");
    }

    // Placeholder method – replace with actual logic to obtain DataTables
    static DataTable[] GetDataTables()
    {
        // Example: create two simple DataTables with matching column names to PDF fields
        DataTable dt1 = new DataTable("Table1");
        dt1.Columns.Add("Name", typeof(string));
        dt1.Columns.Add("Address", typeof(string));
        dt1.Rows.Add("Alice", "123 Main St");

        DataTable dt2 = new DataTable("Table2");
        dt2.Columns.Add("Name", typeof(string));
        dt2.Columns.Add("Address", typeof(string));
        dt2.Rows.Add("Bob", "456 Oak Ave");

        return new[] { dt1, dt2 };
    }
}
