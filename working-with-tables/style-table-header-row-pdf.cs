using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_header_style.pdf";

        // Ensure deterministic disposal of the Document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a new page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and define column widths
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                ColumnWidths = "100 150 200"
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // ----- Header Row -----
            Aspose.Pdf.Row headerRow = table.Rows.Add();

            string[] headers = { "Product", "Quantity", "Price" };
            foreach (string header in headers)
            {
                // Create a cell for each header
                Aspose.Pdf.Cell cell = headerRow.Cells.Add();

                // Add header text
                cell.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(header));

                // Apply background color to the header cell
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;

                // Apply bold font style to the header cell using FontRepository
                TextState ts = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 12F,
                    FontStyle = FontStyles.Bold
                };
                cell.DefaultCellTextState = ts;
            }

            // ----- Example Body Row -----
            Aspose.Pdf.Row bodyRow = table.Rows.Add();
            string[] data = { "Widget", "10", "$5.00" };
            foreach (string d in data)
            {
                Aspose.Pdf.Cell cell = bodyRow.Cells.Add();
                cell.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(d));
            }

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
