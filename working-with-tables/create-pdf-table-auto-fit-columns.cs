using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table();

            // Enable auto‑fit to content for all columns
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // OPTIONAL: set a border for visual clarity
            table.Border = new BorderInfo(BorderSide.All, 0.5f);

            // Add header row
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Color.LightGray; // use Aspose.Pdf.Color
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Product") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Quantity") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Price") } });

            // Add some data rows
            AddDataRow(table, "Widget A", "10", "$5.00");
            AddDataRow(table, "Widget B", "7", "$8.50");
            AddDataRow(table, "Widget C", "15", "$3.20");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }

    // Helper method to add a data row to the table
    static void AddDataRow(Table table, string product, string quantity, string price)
    {
        Row row = table.Rows.Add();
        row.Cells.Add(new Cell { Paragraphs = { new TextFragment(product) } });
        row.Cells.Add(new Cell { Paragraphs = { new TextFragment(quantity) } });
        row.Cells.Add(new Cell { Paragraphs = { new TextFragment(price) } });
    }
}