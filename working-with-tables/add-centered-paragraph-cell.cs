using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfExamples
{
    class AddCenteredParagraphToTableCell
    {
        static void Main(string[] args)
        {
            // Create a sample PDF file (self‑contained example)
            using (Document sampleDoc = new Document())
            {
                sampleDoc.Pages.Add();
                sampleDoc.Save("input.pdf");
            }

            // Open the sample PDF and add a table with a centered paragraph in a cell
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = (Page)doc.Pages[1];

                // Create a table with a single column
                Table table = new Table();
                table.ColumnWidths = "200"; // width of the column

                // Add a row to the table
                Row row = table.Rows.Add();

                // Add a cell to the row
                Cell cell = row.Cells.Add();

                // Create a text fragment (paragraph) and center it
                TextFragment paragraph = new TextFragment("Centered text");
                paragraph.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the paragraph to the cell
                cell.Paragraphs.Add(paragraph);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the resulting PDF
                doc.Save("output.pdf");
            }
        }
    }
}
