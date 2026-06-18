using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF containing text and a simple table
        using (Document createDoc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = createDoc.Pages.Add();

            // Add a text fragment
            TextFragment text = new TextFragment("Sample PDF with a table.");
            page.Paragraphs.Add(text);

            // Create a table with two columns
            Table table = new Table();
            table.ColumnWidths = "100 100"; // two columns of equal width

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF that will be used as input for conversion
            createDoc.Save("input.pdf");
        }

        // Step 2: Load the PDF and convert it to DOCX while preserving layout
        using (Document doc = new Document("input.pdf"))
        {
            doc.Save("output.docx", SaveFormat.DocX);
        }

        Console.WriteLine("PDF successfully converted to DOCX.");
    }
}