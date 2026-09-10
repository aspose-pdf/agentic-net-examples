using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextState is defined here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF to modify (can be empty)
        const string outputPath = "centered_table.pdf";

        // Ensure the input file exists; if not, create a blank PDF to work with
        if (!File.Exists(inputPath))
        {
            using (Document emptyDoc = new Document())
            {
                emptyDoc.Pages.Add(); // add a single blank page
                emptyDoc.Save(inputPath);
            }
        }

        // Open the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and center it horizontally on the page
            Table table = new Table
            {
                // HorizontalAlignment.Center aligns the whole table to the page center
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Optional: define column widths (e.g., two equal columns)
            // table.ColumnWidths = "100 100"; // or new float[] { 100, 100 };

            // First row (header)
            Row row1 = table.Rows.Add();
            Cell cell11 = row1.Cells.Add();
            cell11.DefaultCellTextState = new TextState { FontSize = 12 };
            cell11.Paragraphs.Add(new TextFragment("Header 1"));
            Cell cell12 = row1.Cells.Add();
            cell12.DefaultCellTextState = new TextState { FontSize = 12 };
            cell12.Paragraphs.Add(new TextFragment("Header 2"));

            // Second row (data)
            Row row2 = table.Rows.Add();
            Cell cell21 = row2.Cells.Add();
            cell21.DefaultCellTextState = new TextState { FontSize = 10 };
            cell21.Paragraphs.Add(new TextFragment("Data A"));
            Cell cell22 = row2.Cells.Add();
            cell22.DefaultCellTextState = new TextState { FontSize = 10 };
            cell22.Paragraphs.Add(new TextFragment("Data B"));

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table centered and saved to '{outputPath}'.");
    }
}
