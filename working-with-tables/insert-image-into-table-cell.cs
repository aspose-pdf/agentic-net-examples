using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string dataDir   = "Data";                     // folder containing the image
        string imagePath = Path.Combine(dataDir, "sample.png");
        string outputPdf = Path.Combine(dataDir, "TableWithImage.pdf");

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (in points). Adjust as required.
                ColumnWidths = "250 250"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // ----- First cell: simple text -----
            Cell textCell = row.Cells.Add();
            TextFragment tf = new TextFragment("This cell contains text.");
            textCell.Paragraphs.Add(tf);

            // ----- Second cell: image -----
            Cell imageCell = row.Cells.Add();

            // Create an Image object, set its source file, and add it to the cell
            Image img = new Image
            {
                File = imagePath
                // Optional: set explicit dimensions
                // FixWidth  = 200,
                // FixHeight = 150
            };
            imageCell.Paragraphs.Add(img);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created: {outputPdf}");
    }
}