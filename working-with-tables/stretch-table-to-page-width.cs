using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table that will stretch to the full page width.
            // Setting ColumnWidths to a percentage string makes the table auto‑fit.
            Table table = new Table
            {
                // Example with three columns that together occupy 100% of the page width
                ColumnWidths = "33% 34% 33%"
            };

            // Optional: add a border to make the table visible
            table.Border = new BorderInfo(BorderSide.All, 0.5f);

            // Add a header row
            Row header = table.Rows.Add();
            Cell h1 = header.Cells.Add();
            h1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell h2 = header.Cells.Add();
            h2.Paragraphs.Add(new TextFragment("Header 2"));
            Cell h3 = header.Cells.Add();
            h3.Paragraphs.Add(new TextFragment("Header 3"));

            // Add a data row
            Row data = table.Rows.Add();
            Cell d1 = data.Cells.Add();
            d1.Paragraphs.Add(new TextFragment("Data 1"));
            Cell d2 = data.Cells.Add();
            d2.Paragraphs.Add(new TextFragment("Data 2"));
            Cell d3 = data.Cells.Add();
            d3.Paragraphs.Add(new TextFragment("Data 3"));

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with stretched table saved to '{outputPath}'.");
    }
}