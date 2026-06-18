using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document sampleDoc = new Document())
        {
            sampleDoc.Pages.Add();
            sampleDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a fixed row height
        using (Document doc = new Document("input.pdf"))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and configure its layout
            Table table = new Table();
            table.ColumnWidths = "100 150 100";
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            table.Border = new BorderInfo(BorderSide.All, 1f);
            table.DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };

            // Header row with fixed height
            Row headerRow = table.Rows.Add();
            headerRow.FixedRowHeight = 30;
            headerRow.DefaultCellTextState = new TextState { FontSize = 12, FontStyle = FontStyles.Bold };
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("ID") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Name") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Score") } });

            // Data rows, each with the same fixed height
            for (int i = 1; i <= 3; i++)
            {
                Row dataRow = table.Rows.Add();
                dataRow.FixedRowHeight = 30;
                dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment(i.ToString()) } });
                dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item " + i) } });
                dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment((i * 10).ToString()) } });
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the updated PDF
            doc.Save("output.pdf");
        }
    }
}