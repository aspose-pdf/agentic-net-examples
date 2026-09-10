using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Create a minimal template PDF that the example can work with.
        // The number of pages is based on the maximum page index required by the naming table.
        DataTable namingTable = GetNamingTable();
        int maxPage = GetMaxPageNumber(namingTable);
        const string templatePath = "template.pdf";
        CreateTemplatePdf(templatePath, maxPage);

        // PdfFileEditor provides page‑level operations without needing a Document instance.
        PdfFileEditor editor = new PdfFileEditor();

        // Ensure the output folder exists once.
        const string outputFolder = "Output";
        Directory.CreateDirectory(outputFolder);

        foreach (DataRow row in namingTable.Rows)
        {
            // Safely retrieve the page number and the custom file name.
            int pageNumber = row.Field<int>("Page");
            string customFileName = row.Field<string>("FileName") ?? $"Page_{pageNumber}.pdf";

            // Build a full output path.
            string outputPath = Path.Combine(outputFolder, customFileName);

            // Extract the specified page and save it using the custom file name.
            // PdfFileEditor.Extract(string inputFile, int[] pages, string outputFile)
            editor.Extract(templatePath, new int[] { pageNumber }, outputPath);
        }

        Console.WriteLine("All pages have been extracted with custom names.");
    }

    // Creates a PDF with the requested number of blank pages.
    private static void CreateTemplatePdf(string path, int pageCount)
    {
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
            {
                doc.Pages.Add();
            }
            doc.Save(path);
        }
    }

    // Returns the highest page number present in the naming table (defaults to 1).
    private static int GetMaxPageNumber(DataTable table)
    {
        int max = 1;
        foreach (DataRow row in table.Rows)
        {
            int page = row.Field<int>("Page");
            if (page > max) max = page;
        }
        return max;
    }

    // Example method that creates a DataTable with page‑to‑file‑name mappings.
    // Replace this with actual data retrieval logic as needed.
    static DataTable GetNamingTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Page", typeof(int));
        table.Columns.Add("FileName", typeof(string));

        // Sample rows – adjust to match your real data source.
        table.Rows.Add(1, "Invoice_001.pdf");
        table.Rows.Add(2, "Invoice_002.pdf");
        table.Rows.Add(3, "Invoice_003.pdf");

        return table;
    }
}
