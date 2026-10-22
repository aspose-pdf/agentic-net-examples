using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "table_centered.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table
            Table table = new Table
            {
                // Set column widths (example: three equal columns)
                ColumnWidths = "100 100 100",

                // Center the table horizontally on the page
                HorizontalAlignment = HorizontalAlignment.Center,

                // Optional: set default border and padding for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),

                // Optional: set default text formatting for all cells
                DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Arial")
                }
            };

            // Add header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Add another data row
            Row data2 = table.Rows.Add();
            data2.Cells.Add("Cell B1");
            data2.Cells.Add("Cell B2");
            data2.Cells.Add("Cell B3");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with centered table saved to '{outputPath}'.");
    }
}