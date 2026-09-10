using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing; // for Position

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a text fragment (BaseParagraph) and add it to the page
            // -------------------------------------------------
            TextFragment paragraph = new TextFragment("Below is a table inserted into the paragraph collection:");
            // Position the fragment (X = 50, Y = 700). Y is measured from the bottom of the page.
            paragraph.Position = new Position(50, 700);
            page.Paragraphs.Add(paragraph);

            // -------------------------------------------------
            // 2. Create a table (also a BaseParagraph)
            // -------------------------------------------------
            Table table = new Table();
            // Define two equal column widths
            table.ColumnWidths = "250 250";

            // ----- First row (header) -----
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));

            // ----- Second row (data) -----
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            dataCell1.Paragraphs.Add(new TextFragment("Data 1"));
            Cell dataCell2 = dataRow.Cells.Add();
            dataCell2.Paragraphs.Add(new TextFragment("Data 2"));

            // -------------------------------------------------
            // 3. Insert the table into the page's paragraph collection
            //    after the previously added text fragment.
            //    Paragraphs.Insert uses a zero‑based index.
            // -------------------------------------------------
            // The text fragment is at index 0, so insert table at index 1
            page.Paragraphs.Insert(1, table);

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with table created successfully.");
    }
}