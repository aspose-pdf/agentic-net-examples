using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for TextState
using Aspose.Pdf.Drawing; // for GraphInfo and BorderSide

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a new table
            Table table = new Table();

            // Configure a double border:
            // 1. Create GraphInfo, enable double line and set total thickness
            GraphInfo graphInfo = new GraphInfo
            {
                IsDoubled = true,   // double line style
                LineWidth = 2       // total border thickness (both lines + space)
            };

            // 2. Create BorderInfo for all sides using the GraphInfo
            BorderInfo doubleBorder = new BorderInfo(BorderSide.All, graphInfo);

            // 3. Assign the border to the table
            table.Border = doubleBorder;

            // Optional: define column widths and add a simple row with two cells
            table.ColumnWidths = "200 200";

            Row row = new Row();
            Cell cell1 = new Cell { DefaultCellTextState = new TextState("Cell 1") };
            Cell cell2 = new Cell { DefaultCellTextState = new TextState("Cell 2") };
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            table.Rows.Add(row);

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with double‑bordered table to '{outputPath}'.");
    }
}