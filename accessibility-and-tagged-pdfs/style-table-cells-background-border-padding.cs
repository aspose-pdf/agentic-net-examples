using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based, but Add() returns the new page)
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Define number of rows and columns
            int rows = 3;
            int cols = 3;

            // Populate the table
            for (int r = 0; r < rows; r++)
            {
                Row row = table.Rows.Add();
                for (int c = 0; c < cols; c++)
                {
                    // Add a cell with sample text
                    Cell cell = row.Cells.Add($"R{r + 1}C{c + 1}");

                    // Set background color using Aspose.Pdf.Color (cross‑platform)
                    cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

                    // Set border thickness (and optional color) using the BorderInfo constructor
                    cell.Border = new BorderInfo(BorderSide.All, 1f);

                    // Set padding (left, top, right, bottom) using MarginInfo
                    cell.Margin = new MarginInfo(5, 5, 5, 5);
                }
            }

            // Save the PDF (Document.Save without options always writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }
}
