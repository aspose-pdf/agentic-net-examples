using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

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

        // Open the sample PDF and add a table with a numbered list inside a cell
        using (Document doc = new Document("input.pdf"))
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table();
            table.ColumnWidths = "200";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create numbered list items as paragraphs
            TextFragment item1 = new TextFragment("1. First item");
            TextFragment item2 = new TextFragment("2. Second item");
            TextFragment item3 = new TextFragment("3. Third item");

            // Insert the paragraphs into the cell
            cell.Paragraphs.Add(item1);
            cell.Paragraphs.Add(item2);
            cell.Paragraphs.Add(item3);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}