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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Add a single row and a single cell
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Add visible text to the cell
            cell.Paragraphs.Add(new TextFragment("Click here for more info"));

            // Define the rectangle area for the hyperlink.
            // Coordinates are in points, origin is bottom‑left of the page.
            // Adjust these values to fit the actual cell position as needed.
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(
                llx: 100,   // left
                lly: 500,   // bottom
                urx: 250,   // right
                ury: 520    // top
            );

            // Create a LinkAnnotation that points to an external URL
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Use GoToURIAction for external web links (preferred over Hyperlink property)
                Action = new GoToURIAction("https://www.example.com")
            };

            // Add the annotation to the cell's paragraph collection.
            // LinkAnnotation derives from BaseParagraph, so it can be added here.
            cell.Paragraphs.Add(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hyperlink saved to '{outputPath}'.");
    }
}