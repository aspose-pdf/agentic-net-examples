using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Instantiate a Table object
            Table table = new Table();

            // Optional: configure table appearance
            // NOTE: ColumnAdjustment.AutoFit is not available in all Aspose.Pdf versions; the default behavior is auto‑fit, so we omit this setting.
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState
            {
                FontSize = 12,
                Font = FontRepository.FindFont("Helvetica")
            };

            // Define column widths (two columns, each 200 points wide)
            table.ColumnWidths = "200 200";

            // ----- Header row -----
            Row headerRow = new Row();

            Cell headerCell1 = new Cell
            {
                DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica-Bold")
                }
            };
            headerCell1.Paragraphs.Add(new TextFragment("Product"));

            Cell headerCell2 = new Cell
            {
                DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    Font = FontRepository.FindFont("Helvetica-Bold")
                }
            };
            headerCell2.Paragraphs.Add(new TextFragment("Price"));

            headerRow.Cells.Add(headerCell1);
            headerRow.Cells.Add(headerCell2);
            table.Rows.Add(headerRow);

            // ----- Data row -----
            Row dataRow = new Row();

            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Widget A"));

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("$50"));

            dataRow.Cells.Add(dataCell1);
            dataRow.Cells.Add(dataCell2);
            table.Rows.Add(dataRow);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}