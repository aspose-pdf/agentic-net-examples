using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal-width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };
            page.Paragraphs.Add(table);

            // Populate the table with five rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();

                // Add three cells to each row with sample text
                for (int j = 1; j <= 3; j++)
                {
                    Cell cell = row.Cells.Add();
                    cell.Paragraphs.Add(new TextFragment($"R{i}C{j}"));
                }

                // After the third row, insert a page break
                if (i == 3)
                {
                    // Add a placeholder row that will start on a new page
                    Row breakRow = table.Rows.Add();
                    breakRow.IsInNewPage = true; // forces this row onto the next page
                }
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}