using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main(string[] args)
    {
        // Create a sample PDF file
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a cell that has custom margins
        using (Document doc = new Document("input.pdf"))
        {
            // Create a table and add it to the first page
            Table table = new Table();
            doc.Pages[1].Paragraphs.Add(table);

            // Add a row
            Row row = table.Rows.Add();

            // Add a cell with some text
            Cell cell = row.Cells.Add();
            TextFragment tf = new TextFragment("Cell with custom margins");
            cell.Paragraphs.Add(tf);

            // Configure cell margins
            MarginInfo margin = new MarginInfo();
            margin.Left = 10;
            margin.Right = 10;
            margin.Top = 5;
            margin.Bottom = 5;
            cell.Margin = margin;

            // Save the result
            doc.Save("output.pdf");
        }
    }
}