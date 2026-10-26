using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Load existing PDF if it exists; otherwise create a new document with a blank page.
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            doc.Pages.Add(); // ensure at least one page
        }

        // Ensure deterministic disposal of the Document.
        using (doc)
        {
            // Create a table that will span multiple pages.
            Table table = new Table
            {
                // Define three equal-width columns.
                ColumnWidths = "100 100 100",
                // Repeat the first row (header) on every new page.
                RepeatingRowsCount = 1
            };

            // Header row – this row will be repeated on each page because of RepeatingRowsCount.
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add many data rows to force the table to flow onto subsequent pages.
            for (int i = 1; i <= 100; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.Cells.Add($"Item {i}A");
                dataRow.Cells.Add($"Item {i}B");
                dataRow.Cells.Add($"Item {i}C");
            }

            // Insert the table into the first page's paragraph collection.
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with repeating table header saved to '{outputPath}'.");
    }
}