using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that will be split into pages.
        const string inputPdfPath = "template.pdf";

        // DataTable containing custom names for each page.
        // Expected columns:
        //   PageNumber (int) – 1‑based page index in the PDF.
        //   FileName   (string) – Desired file name without extension.
        DataTable namingTable = new DataTable();
        namingTable.Columns.Add("PageNumber", typeof(int));
        namingTable.Columns.Add("FileName", typeof(string));

        // Example data – replace with real data source.
        namingTable.Rows.Add(1, "Invoice_2023_001");
        namingTable.Rows.Add(2, "Invoice_2023_002");
        namingTable.Rows.Add(3, "Invoice_2023_003");
        // ... add rows for all pages ...

        // Ensure the input file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Split the PDF into individual page streams.
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdfPath);

        // Verify that we have a stream for each page.
        if (pageStreams.Length != namingTable.Rows.Count)
        {
            Console.Error.WriteLine("Mismatch between number of pages and rows in the naming table.");
            // Dispose streams before exiting.
            foreach (var s in pageStreams) s.Dispose();
            return;
        }

        // Save each page using the custom file name from the DataTable.
        for (int i = 0; i < pageStreams.Length; i++)
        {
            // Page numbers are 1‑based; DataTable stores the same.
            int pageNumber = i + 1;

            // Find the row that matches the current page number.
            DataRow[] rows = namingTable.Select($"PageNumber = {pageNumber}");
            if (rows.Length == 0)
            {
                Console.Error.WriteLine($"No naming entry for page {pageNumber}; skipping.");
                pageStreams[i].Dispose();
                continue;
            }

            string customName = rows[0]["FileName"].ToString();
            string outputPath = $"{customName}.pdf";

            // Write the memory stream to a file.
            using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                pageStreams[i].WriteTo(file);
            }

            // Dispose the stream now that it has been saved.
            pageStreams[i].Dispose();

            Console.WriteLine($"Saved page {pageNumber} as '{outputPath}'.");
        }
    }
}