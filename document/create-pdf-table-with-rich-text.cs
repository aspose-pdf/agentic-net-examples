using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "RichTextTable.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (optional)
                Left = 50,
                Top = 700,
                // Define column widths (in points)
                ColumnWidths = "150 150"
            };

            // -------------------------
            // First Row – Header cells
            // -------------------------
            // Create first header cell with bold, blue text
            TextFragment header1 = new TextFragment("Product");
            header1.TextState.Font = FontRepository.FindFont("Helvetica");
            header1.TextState.FontSize = 12;
            header1.TextState.FontStyle = FontStyles.Bold;
            header1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Create second header cell with bold, blue text
            TextFragment header2 = new TextFragment("Price");
            header2.TextState.Font = FontRepository.FindFont("Helvetica");
            header2.TextState.FontSize = 12;
            header2.TextState.FontStyle = FontStyles.Bold;
            header2.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the header row
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add(header1);
            headerRow.Cells.Add(header2);

            // -------------------------
            // Data Row 1
            // -------------------------
            // Cell with normal text and red color
            TextFragment cell11 = new TextFragment("Widget A");
            cell11.TextState.Font = FontRepository.FindFont("Helvetica");
            cell11.TextState.FontSize = 10;
            cell11.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Cell with italic, green text
            TextFragment cell12 = new TextFragment("$25.00");
            cell12.TextState.Font = FontRepository.FindFont("Helvetica");
            cell12.TextState.FontSize = 10;
            cell12.TextState.FontStyle = FontStyles.Italic;
            cell12.TextState.ForegroundColor = Aspose.Pdf.Color.Green;

            Row dataRow1 = table.Rows.Add();
            dataRow1.Cells.Add(cell11);
            dataRow1.Cells.Add(cell12);

            // -------------------------
            // Data Row 2 – demonstrates multiple segments in a single cell
            // -------------------------
            // Create a fragment with three segments: normal, bold, normal
            TextFragment cell21 = new TextFragment();
            // First segment – normal text
            TextSegment seg1 = new TextSegment("Widget B (");
            seg1.TextState.Font = FontRepository.FindFont("Helvetica");
            seg1.TextState.FontSize = 10;
            seg1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            // Second segment – bold text
            TextSegment seg2 = new TextSegment("Special");
            seg2.TextState.Font = FontRepository.FindFont("Helvetica");
            seg2.TextState.FontSize = 10;
            seg2.TextState.FontStyle = FontStyles.Bold;
            seg2.TextState.ForegroundColor = Aspose.Pdf.Color.Purple;
            // Third segment – closing parenthesis
            TextSegment seg3 = new TextSegment(")");
            seg3.TextState.Font = FontRepository.FindFont("Helvetica");
            seg3.TextState.FontSize = 10;
            seg3.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add segments to the fragment
            cell21.Segments.Add(seg1);
            cell21.Segments.Add(seg2);
            cell21.Segments.Add(seg3);

            // Second cell with underlined text
            TextFragment cell22 = new TextFragment("$40.00");
            cell22.TextState.Font = FontRepository.FindFont("Helvetica");
            cell22.TextState.FontSize = 10;
            cell22.TextState.Underline = true; // Underline is a boolean property, not a FontStyle
            cell22.TextState.ForegroundColor = Aspose.Pdf.Color.Orange;

            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add(cell21);
            dataRow2.Cells.Add(cell22);

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rich‑text table saved to '{outputPath}'.");
    }
}
