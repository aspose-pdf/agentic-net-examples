using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";   // existing PDF or can be a blank PDF
        const string outputPath = "rotated_table.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Load the document (using rule: load with using)
        using (Document doc = new Document(inputPath))
        {
            // Add a new page to host the rotated table
            Page page = doc.Pages.Add();

            // Create a simple DataTable to populate the Aspose.Pdf.Table
            DataTable dt = new DataTable();
            dt.Columns.Add("Product");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Price");
            dt.Rows.Add("Widget A", "10", "$5.00");
            dt.Rows.Add("Widget B", "7",  "$8.50");
            dt.Rows.Add("Widget C", "3",  "$12.00");

            // Create the Aspose.Pdf.Table and import the DataTable
            Table pdfTable = new Table();
            pdfTable.ColumnWidths = "100 100 100"; // three equal columns
            pdfTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black);
            pdfTable.ImportDataTable(dt, true, 0, 0);

            // Position the table on the page (before rotation)
            pdfTable.Left = 50;   // X coordinate
            pdfTable.Top  = 50;   // Y coordinate

            // Apply a 90‑degree rotation matrix to the page's content.
            // Aspose.Pdf provides a static helper to create the matrix.
            // The matrix is then assigned to the page's transformation matrix
            // via the internal Content stream. The simplest way is to set the
            // page's Rotate property, which internally uses the same matrix.
            page.Rotate = Rotation.on90; // rotates the whole page (including the table)

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(pdfTable);

            // Save the modified document (using rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document with rotated table saved to '{outputPath}'.");
    }
}