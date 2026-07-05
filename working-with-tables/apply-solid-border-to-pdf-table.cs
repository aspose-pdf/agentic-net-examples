using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // For text handling (optional)

// Create a new PDF document and ensure deterministic disposal
using (Document doc = new Document())
{
    // Add a blank page to the document
    Page page = doc.Pages.Add();

    // Create a table instance
    Table table = new Table();

    // Define three equal column widths (adjust as needed)
    table.ColumnWidths = "100 100 100";

    // Add a single row with three cells
    Row row = table.Rows.Add();
    row.Cells.Add("Cell 1");
    row.Cells.Add("Cell 2");
    row.Cells.Add("Cell 3");

    // Apply a solid black border to the entire table
    // BorderInfo(BorderSide, width, color) creates a uniform border on all sides
    table.Border = new BorderInfo(Aspose.Pdf.BorderSide.All, 1f, Aspose.Pdf.Color.Black);

    // Include the border width in column calculations (optional but common)
    table.IsBordersIncluded = true;

    // Add the configured table to the page's paragraph collection
    page.Paragraphs.Add(table);

    // Save the PDF document to disk
    doc.Save("TableWithBorder.pdf");
}