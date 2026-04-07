using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string outputPath = "TableWithShadow.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                // Set table border (optional)
                Border = new BorderInfo(BorderSide.All, 1, Color.Black),

                // Set column widths (space‑separated string)
                ColumnWidths = "100 150 200",

                // Set table position on the page
                Left = 50,
                Top = 700
            };

            // Add a header row
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Color.LightGray;
            headerRow.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                FontStyle = FontStyles.Bold,
                ForegroundColor = Color.White
            };
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 10,
                ForegroundColor = Color.Black
            };
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // NOTE:
            // Aspose.Pdf.Table does NOT expose a ShadowEffect property.
            // Therefore a native shadow cannot be applied directly to the table.
            // If a visual shadow is required, it can be simulated by adding a
            // separate graphical element (e.g., a semi‑transparent rectangle)
            // behind the table using the Facades API or by stamping an image
            // that contains the desired shadow effect.

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}