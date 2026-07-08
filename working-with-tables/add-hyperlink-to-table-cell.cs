using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages[1];

            // Create a simple table with one cell containing the display text
            Table table = new Table();
            table.ColumnWidths = "200"; // width of the single column
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            Cell cell = table.Rows.Add().Cells.Add("Click here");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Define the rectangle that covers the cell.
            // In a real scenario you would obtain the exact rectangle after layout,
            // but for this example we use placeholder coordinates.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a LinkAnnotation that points to an external URL.
            // Use GoToURIAction because LinkAnnotation.Hyperlink is not a string (rule: hyperlink-property-is-not-a-string).
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction("https://www.example.com"),
                Color  = Aspose.Pdf.Color.Blue
            };

            // Set a border with zero width (transparent) – Border requires the parent annotation in its constructor (rule: no-graphic-state-parameterless-ctor).
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hyperlinked PDF saved to '{outputPath}'.");
    }
}