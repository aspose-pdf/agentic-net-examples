using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (llx, lly, urx, ury)
                // Adjust as needed for your layout
                ColumnWidths = "100 200 150"
            };
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // *** Auto‑fit row height ***
            // Do NOT set FixedRowHeight – leaving it unset (default 0) lets the row
            // height be determined by its content (intrinsic height).
            // Optionally, ensure MinRowHeight is zero so there is no minimum constraint.
            row.MinRowHeight = 0; // Auto‑fit (no minimum height)

            // Add cells with content that may require multiple lines
            Cell cell1 = row.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Short text"));

            Cell cell2 = row.Cells.Add();
            // This cell contains a longer paragraph; the row height will expand automatically.
            cell2.Paragraphs.Add(new TextFragment(
                "This is a longer piece of text that should cause the row to increase its height " +
                "automatically to accommodate the wrapped content without any manual height settings."));

            Cell cell3 = row.Cells.Add();
            cell3.Paragraphs.Add(new TextFragment("Another cell"));

            // Save the PDF
            string outputPath = "AutoFitRowHeight.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}