using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for proper disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure the document has at least one page
            Aspose.Pdf.Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a new Table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();

            // Set basic table properties (alignment, column widths, etc.)
            table.ColumnWidths = "100 100 100"; // three columns, each 100 points wide
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
            table.Border = new BorderInfo(BorderSide.All, 2f, Aspose.Pdf.Color.DarkGray);
            table.DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };

            // Add a header row
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Aspose.Pdf.Color.LightGray;
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Add a data row
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");
            dataRow.Cells.Add("Cell 3");

            // NOTE: Aspose.Pdf.Table does NOT expose a ShadowEffect property.
            // Shadow effects are available for annotations (via Border.Effect) but not for tables.
            // Therefore a native shadow cannot be applied directly to a Table.
            // If a visual shadow is required, consider drawing a rectangle behind the table
            // with a darker color and a slight offset to simulate a shadow.

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}