using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the sample PDF and add a table with a hyperlink inside a cell
        using (Document doc = new Document("input.pdf"))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table with a single column
            Table table = new Table();
            table.ColumnWidths = "200";

            // Add a row and a cell containing placeholder text
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add("Click here");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Define a rectangle that roughly covers the cell area
            // (adjust the coordinates as needed for real layouts)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a LinkAnnotation with a web hyperlink
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            link.Hyperlink = new WebHyperlink("https://www.example.com");
            link.Color = Aspose.Pdf.Color.Blue;
            link.Border = new Border(link);

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the updated document
            doc.Save("output.pdf");
        }
    }
}