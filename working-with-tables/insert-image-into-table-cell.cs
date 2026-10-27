using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_image.pdf";
        const string imagePath = "sample.jpg"; // external image file

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Set basic document metadata (avoid properties that may not exist in older versions)
            doc.Info.Title = "PDF with Image in Table Cell";

            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with a single column (width can be adjusted as needed)
            Table table = new Table
            {
                ColumnWidths = "200" // width of the single column in points
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell (TD) to the row
            Cell cell = row.Cells.Add();

            // Load the external image and add it to the cell using the Image class
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                Image img = new Image
                {
                    ImageStream = imgStream,
                    // Optional: set explicit size (in points). Adjust as required.
                    FixWidth = 180,
                    FixHeight = 180
                };
                // Add the image to the cell's paragraph collection
                cell.Paragraphs.Add(img);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with the image inserted into a table cell.");
    }
}
