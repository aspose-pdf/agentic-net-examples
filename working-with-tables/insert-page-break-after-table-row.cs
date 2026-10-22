using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns (150 points each)
            Table table = new Table { ColumnWidths = "150 150 150" };

            // Add five rows to the table
            for (int rowIndex = 1; rowIndex <= 5; rowIndex++)
            {
                // Create a new row (Aspose.Pdf uses the Row class)
                Row row = table.Rows.Add();

                // Populate the row with three cells
                for (int colIndex = 1; colIndex <= 3; colIndex++)
                {
                    row.Cells.Add($"Row {rowIndex}, Col {colIndex}");
                }

                // Insert a page break after the third row
                if (rowIndex == 3)
                {
                    // NewPageFragment is a Paragraph that forces a page break.
                    // Adding a TextFragment with a form‑feed character ("\f") works as a simple alternative.
                    row.Cells.Add(new TextFragment("\f"));
                }
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page break saved to '{outputPath}'.");
    }
}
