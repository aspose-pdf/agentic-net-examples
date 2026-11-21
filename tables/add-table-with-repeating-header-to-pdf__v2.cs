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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[1];

            // Create a new table.
            Table table = new Table();

            // Ensure the header row repeats on each page if the table spans multiple pages.
            table.RepeatingRowsCount = 1;

            // Define column widths (optional, adjust as needed).
            // Here we create three equal columns.
            table.ColumnWidths = "100 100 100";

            // Add the header row.
            Row headerRow = table.Rows.Add();

            // First header cell with bold HTML text.
            Cell cell1 = headerRow.Cells.Add();
            cell1.Paragraphs.Add(new HtmlFragment("<b>Header 1</b>"));

            // Second header cell with bold HTML text.
            Cell cell2 = headerRow.Cells.Add();
            cell2.Paragraphs.Add(new HtmlFragment("<b>Header 2</b>"));

            // Third header cell with bold HTML text.
            Cell cell3 = headerRow.Cells.Add();
            cell3.Paragraphs.Add(new HtmlFragment("<b>Header 3</b>"));

            // Add the table to the page's paragraph collection.
            page.Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added and PDF saved to '{outputPath}'.");
    }
}