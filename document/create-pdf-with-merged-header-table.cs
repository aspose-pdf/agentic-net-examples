using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment and TextState

class Program
{
    static void Main()
    {
        const string outputPath = "merged_header_table.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table with three equal‑width columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100"
            };

            // ----- Header row with a merged cell spanning two columns -----
            Row headerRow = table.Rows.Add();

            // Cell that *visually* spans the first two columns (ColumnSpan not available in this version)
            Cell mergedHeader = headerRow.Cells.Add("Merged Header");
            mergedHeader.BackgroundColor = Aspose.Pdf.Color.LightGray;
            mergedHeader.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };
            // Add a placeholder cell next to the merged header and hide its borders so the two cells appear as one
            Cell placeholder = headerRow.Cells.Add("");
            placeholder.BackgroundColor = Aspose.Pdf.Color.LightGray;
            placeholder.DefaultCellTextState = mergedHeader.DefaultCellTextState;
            placeholder.Border = new BorderInfo(BorderSide.None);

            // Cell for the third column
            Cell headerCell3 = headerRow.Cells.Add("Third Column");
            headerCell3.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerCell3.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // ----- First data row -----
            Row dataRow1 = table.Rows.Add();
            dataRow1.Cells.Add("Row1 Col1");
            dataRow1.Cells.Add("Row1 Col2");
            dataRow1.Cells.Add("Row1 Col3");

            // ----- Second data row -----
            Row dataRow2 = table.Rows.Add();
            dataRow2.Cells.Add("Row2 Col1");
            dataRow2.Cells.Add("Row2 Col2");
            dataRow2.Cells.Add("Row2 Col3");

            // Add the constructed table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with merged header cells saved to '{outputPath}'.");
    }
}
