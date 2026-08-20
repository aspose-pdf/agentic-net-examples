using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Create a simple table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Define column widths – this replaces the read‑only Cell.Width property
            // Table.ColumnWidths expects a comma‑separated string, not a float array.
            table.ColumnWidths = "200";

            // Add a row
            Row row = table.Rows.Add();

            // Add a cell with visible text
            Cell cell = row.Cells.Add("Click here");

            // Define the rectangle that will act as the clickable area.
            // In a real scenario you would calculate this based on the cell's position.
            // Here we use a fixed rectangle for demonstration purposes.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 200, 520);

            // Create a LinkAnnotation that points to an external URL.
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                Action = new GoToURIAction("https://www.example.com"),
                Color = Aspose.Pdf.Color.Blue
            };

            // Optional: add a visible border around the link area
            link.Border = new Border(link) { Width = 1 };

            // Add the annotation to the page (the annotation is visually linked to the cell text)
            page.Annotations.Add(link);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hyperlink at '{outputPath}'.");
    }
}
