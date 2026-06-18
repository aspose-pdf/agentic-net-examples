using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a table
        string inputPath = "input.pdf";
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Table table = new Table();
            // Define two columns of equal width
            table.ColumnWidths = "200 200";

            // First row – short content (fixed height not required)
            Row shortRow = table.Rows.Add();
            shortRow.Cells.Add(new TextFragment("Short"));
            shortRow.Cells.Add(new TextFragment("Data"));

            // Second row – long content that should wrap and cause the row height to grow automatically
            Row longRow = table.Rows.Add();
            // Do NOT set FixedRowHeight; let the row size itself based on its content
            longRow.Cells.Add(new TextFragment("This is a very long text that should wrap and cause the row height to increase automatically based on its content."));
            longRow.Cells.Add(new TextFragment("More long text that will also wrap and increase the row height accordingly."));
            // Optional: set a default text state for the cells (font size, etc.)
            longRow.DefaultCellTextState = new TextState { FontSize = 12 };

            // Add the table to the page and save the document
            page.Paragraphs.Add(table);
            doc.Save(inputPath);
        }

        // Step 2: Reopen the created PDF and save it as the final output
        using (Document doc = new Document(inputPath))
        {
            doc.Save("output.pdf");
        }
    }
}