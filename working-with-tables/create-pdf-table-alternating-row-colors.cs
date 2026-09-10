using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for Color definitions if needed

class Program
{
    static void Main()
    {
        const string outputPath = "table_alternating.pdf";

        // Document lifecycle must be managed with using
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f) // thin border around cells
            };

            // Add a header row with a distinct background
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.LightGray;
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add data rows and apply alternating background colors
            for (int i = 0; i < 10; i++)
            {
                Row row = table.Rows.Add();

                // Even rows get a light gray background, odd rows stay white
                if (i % 2 == 0)
                    row.BackgroundColor = Color.FromRgb(0.9f, 0.9f, 0.9f); // light gray
                else
                    row.BackgroundColor = Color.White;

                // Populate cells in the current row
                row.Cells.Add($"Row {i + 1} Col 1");
                row.Cells.Add($"Row {i + 1} Col 2");
                row.Cells.Add($"Row {i + 1} Col 3");
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF (uses the standard Document.Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with alternating row colors to '{outputPath}'.");
    }
}