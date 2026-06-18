using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a multiline cell
        using (Document doc = new Document("input.pdf"))
        {
            // Create a table
            Table table = new Table();

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Insert multiple TextFragment objects separated by line‑break fragments
            cell.Paragraphs.Add(new TextFragment("First line"));
            cell.Paragraphs.Add(new TextFragment("\n"));
            cell.Paragraphs.Add(new TextFragment("Second line"));
            cell.Paragraphs.Add(new TextFragment("\n"));

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the updated PDF
            doc.Save("output.pdf");
        }
    }
}