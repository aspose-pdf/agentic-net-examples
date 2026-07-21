using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a text fragment (BaseParagraph) and add it to the page
            // -------------------------------------------------
            TextFragment fragment = new TextFragment("This paragraph precedes a table:")
            {
                // Position the fragment on the page (X = 50, Y = 750)
                Position = new Position(50, 750)
            };
            page.Paragraphs.Add(fragment);

            // -------------------------------------------------
            // 2. Create a table (BaseParagraph) and populate it
            // -------------------------------------------------
            Table table = new Table
            {
                // Optional: set column widths (comma‑separated or space‑separated string)
                ColumnWidths = "200 200"
            };

            // Add first row
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Header 1");
            row1.Cells.Add("Header 2");

            // Add second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell A1");
            row2.Cells.Add("Cell A2");

            // Add third row
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Cell B1");
            row3.Cells.Add("Cell B2");

            // -------------------------------------------------
            // 3. Insert the table after the fragment
            //    Paragraphs.Insert uses zero‑based indexing.
            // -------------------------------------------------
            // Index 1 inserts the table after the first paragraph (index 0)
            page.Paragraphs.Insert(1, table);

            // -------------------------------------------------
            // 4. Save the document
            // -------------------------------------------------
            doc.Save("TableInsideParagraph.pdf");
        }

        Console.WriteLine("PDF with table inside paragraph created successfully.");
    }
}
