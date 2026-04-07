using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged; // Tagged PDF classes

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a table with one column (width 200 points)
            Table table = new Table();
            table.ColumnWidths = "200";

            // Add a row to the table
            Row row = table.Rows.Add();
            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Add the visual table to the page
            page.Paragraphs.Add(table);

            // Access the tagged content of the document
            TaggedContent taggedContent = pdfDocument.TaggedContent;
            StructureElement root = taggedContent.RootElement;

            // Create a table structure element that mirrors the visual table
            TableElement tableStruct = taggedContent.CreateTableElement();
            root.AppendChild(tableStruct);

            // Create a table row structure element
            TableRowElement trStruct = taggedContent.CreateTableRowElement();
            tableStruct.AppendChild(trStruct);

            // Create a table cell (TD) structure element
            TableCellElement tdStruct = taggedContent.CreateTableCellElement();
            trStruct.AppendChild(tdStruct);

            // Create an illustration element inside the cell
            IllustrationElement illustration = taggedContent.CreateIllustrationElement();
            tdStruct.AppendChild(illustration);

            // Path to the external image file
            string imagePath = "sample.jpg";

            // Insert the image into the illustration element using ImageFragment constructor
            ImageFragment imgFragment = new ImageFragment(imagePath);
            illustration.SetImage(imgFragment);

            // Save the resulting PDF
            pdfDocument.Save("output.pdf");
        }
    }
}
