using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for BorderInfo, BorderSide, MarginInfo

class Program
{
    static void Main()
    {
        const string outputPath = "rich_table.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a table (Table is a BaseParagraph that can be added to a page)
            Table table = new Table
            {
                // Optional visual settings
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f), // Fixed: use BorderSide and correct constructor
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Define column widths (optional, space‑separated values)
            table.ColumnWidths = "120 150 180";

            // ---------- First row ----------
            Row row1 = table.Rows.Add();

            // Cell (1,1) – bold red text
            Cell cell11 = row1.Cells.Add();
            TextFragment tf1 = new TextFragment("Bold Red");
            tf1.TextState.Font = FontRepository.FindFont("Helvetica");
            tf1.TextState.FontSize = 12;
            tf1.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            tf1.TextState.FontStyle = FontStyles.Bold;
            cell11.Paragraphs.Add(tf1);

            // Cell (1,2) – italic blue text
            Cell cell12 = row1.Cells.Add();
            TextFragment tf2 = new TextFragment("Italic Blue");
            tf2.TextState.Font = FontRepository.FindFont("Helvetica");
            tf2.TextState.FontSize = 12;
            tf2.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            tf2.TextState.FontStyle = FontStyles.Italic;
            cell12.Paragraphs.Add(tf2);

            // Cell (1,3) – underline green text
            Cell cell13 = row1.Cells.Add();
            TextFragment tf3 = new TextFragment("Underline Green");
            tf3.TextState.Font = FontRepository.FindFont("Helvetica");
            tf3.TextState.FontSize = 12;
            tf3.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            tf3.TextState.Underline = true;
            cell13.Paragraphs.Add(tf3);

            // ---------- Second row ----------
            Row row2 = table.Rows.Add();

            // Cell (2,1) – larger orange text
            Cell cell21 = row2.Cells.Add();
            TextFragment tf4 = new TextFragment("Large Orange");
            tf4.TextState.Font = FontRepository.FindFont("Helvetica");
            tf4.TextState.FontSize = 16;
            tf4.TextState.ForegroundColor = Aspose.Pdf.Color.Orange;
            cell21.Paragraphs.Add(tf4);

            // Cell (2,2) – bold italic purple text
            Cell cell22 = row2.Cells.Add();
            TextFragment tf5 = new TextFragment("Bold Italic Purple");
            tf5.TextState.Font = FontRepository.FindFont("Helvetica");
            tf5.TextState.FontSize = 12;
            tf5.TextState.ForegroundColor = Aspose.Pdf.Color.Purple;
            tf5.TextState.FontStyle = FontStyles.Bold | FontStyles.Italic;
            cell22.Paragraphs.Add(tf5);

            // Cell (2,3) – empty (optional placeholder)
            Cell cell23 = row2.Cells.Add();

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document
            doc.Save(outputPath);
        }
    }
}
