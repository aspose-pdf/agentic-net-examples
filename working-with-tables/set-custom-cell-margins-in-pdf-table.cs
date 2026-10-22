using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "TableWithMargins.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and set its column widths
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                ColumnWidths = "100 150 100"
            };

            // Define a MarginInfo object to be used for cell padding
            Aspose.Pdf.MarginInfo cellMargin = new Aspose.Pdf.MarginInfo
            {
                Left = 5,
                Right = 5,
                Top = 2,
                Bottom = 2
            };

            // Apply the margin (padding) to all cells by setting the table's default cell padding
            table.DefaultCellPadding = cellMargin;

            // First row (header)
            Aspose.Pdf.Row row1 = table.Rows.Add();
            Aspose.Pdf.Cell cell11 = row1.Cells.Add("Header 1");
            cell11.BackgroundColor = Aspose.Pdf.Color.LightGray;
            Aspose.Pdf.Cell cell12 = row1.Cells.Add("Header 2");
            cell12.BackgroundColor = Aspose.Pdf.Color.LightGray;
            Aspose.Pdf.Cell cell13 = row1.Cells.Add("Header 3");
            cell13.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Second row
            Aspose.Pdf.Row row2 = table.Rows.Add();
            row2.Cells.Add("Row 1, Col 1");
            row2.Cells.Add("Row 1, Col 2");
            row2.Cells.Add("Row 1, Col 3");

            // Third row
            Aspose.Pdf.Row row3 = table.Rows.Add();
            row3.Cells.Add("Row 2, Col 1");
            row3.Cells.Add("Row 2, Col 2");
            row3.Cells.Add("Row 2, Col 3");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table and cell margins saved to '{outputPath}'.");
    }
}
