using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the table will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Create a new Table instance
            Table table = new Table();

            // NOTE: Table does NOT have a Position property. Position is defined by the
            // Left and Top properties. If you need to set the location using a PointF,
            // you can create a PointF and assign its X/Y to Left/Top.
            // Example coordinates (in points):
            float x = 100f; // X coordinate from the left edge of the page
            float y = 500f; // Y coordinate from the bottom edge of the page

            table.Left = x;
            table.Top  = y;

            // Optional: set table appearance
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("Cell A1");
            data.Cells.Add("Cell A2");
            data.Cells.Add("Cell A3");

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted and saved to '{outputPath}'.");
    }
}