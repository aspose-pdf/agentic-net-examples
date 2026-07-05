using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPath = "table_check.pdf";

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure basic properties
            Table table = new Table
            {
                // Define two columns of equal width
                ColumnWidths = "100 100",
                // Initially let Aspose.Pdf decide if the table should break
                IsBroken = false
            };

            // First row with simple cells
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Cell 1");
            row1.Cells.Add("Cell 2");

            // Second row with longer content that may cause a break
            Row row2 = table.Rows.Add();
            row2.Cells.Add("This is a longer piece of text that could push the table onto the next page.");
            row2.Cells.Add("Another cell");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Check whether Aspose.Pdf determined the table will be broken
            bool willBreak = table.IsBroken;
            Console.WriteLine($"Table.IsBroken (auto‑determined) = {willBreak}");

            // Force the table to be broken for demonstration purposes
            table.IsBroken = true;
            Console.WriteLine($"Table.IsBroken (after manual set) = {table.IsBroken}");

            // Save the PDF (output format is always PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}