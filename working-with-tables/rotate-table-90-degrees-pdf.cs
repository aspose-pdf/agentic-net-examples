using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";   // optional existing PDF, can be empty
        const string outputPath = "rotated_table.pdf";

        // Ensure the input file exists; if not, create a blank document
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add(); // add a single empty page
                blank.Save(inputPath);
            }
        }

        // Open the document and work within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first existing page (or the only page we just created)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Create a simple visual Table (Aspose.Pdf.Table) and populate it
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Position the table at (0,0) before rotation; size will be adjusted automatically
                ColumnWidths = "100 100 100", // three columns, each 100 points wide
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");
            header.BackgroundColor = Color.LightGray;

            // Data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Item {i}A");
                row.Cells.Add($"Item {i}B");
                row.Cells.Add($"Item {i}C");
            }

            // ------------------------------------------------------------
            // Apply a 90-degree rotation transformation before drawing the table
            // ------------------------------------------------------------
            // Save the current graphics state (gsave)
            page.Contents.Add(new GSave());

            // Create a matrix that rotates 90° clockwise and translates the content
            // Matrix layout: [a b c d e f] => [0 1 -1 0 pageHeight 0]
            Matrix rotationMatrix = new Matrix(new double[] { 0, 1, -1, 0, page.PageInfo.Height, 0 });

            // Apply the matrix
            page.Contents.Add(new ConcatenateMatrix(rotationMatrix));

            // Add the table while the rotated coordinate system is active
            page.Paragraphs.Add(table);

            // Restore the original graphics state (grestore)
            page.Contents.Add(new GRestore());

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated table PDF saved to '{outputPath}'.");
    }
}
