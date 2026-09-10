using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_centered_paragraph.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and set its column widths
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Position the table on the page
                Left = 50,
                Top = 700,
                // Define two equal-width columns
                ColumnWidths = "200 200"
            };

            // Add a row to the table
            Aspose.Pdf.Row row = table.Rows.Add();

            // Add a cell to the row
            Aspose.Pdf.Cell cell = row.Cells.Add();

            // Create a text fragment (paragraph) with centered alignment
            Aspose.Pdf.Text.TextFragment paragraph = new Aspose.Pdf.Text.TextFragment("Centered text");
            paragraph.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center;

            // Add the paragraph to the cell's paragraph collection
            cell.Paragraphs.Add(paragraph);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}