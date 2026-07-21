using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For TextState if needed

class Program
{
    static void Main()
    {
        const string outputPath = "centered_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the table
            Page page = doc.Pages.Add();

            // Create a table instance
            Table table = new Table();

            // Align the entire table to the center of the page
            table.HorizontalAlignment = HorizontalAlignment.Center;

            // Optional: define column widths (2 columns, equal width)
            table.ColumnWidths = "200 200";

            // Add a header row
            Row header = table.Rows.Add();
            Cell headerCell1 = header.Cells.Add("Header 1");
            Cell headerCell2 = header.Cells.Add("Header 2");
            // Make header text bold
            headerCell1.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
            headerCell2.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell B1");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with centered table saved to '{outputPath}'.");
    }
}