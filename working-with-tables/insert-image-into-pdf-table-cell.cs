using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.png";

        if (!File.Exists(inputPdfPath) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page to work with
            Page page = doc.Pages[1];

            // Create a table with a single column
            Table table = new Table();
            table.ColumnWidths = "200"; // width of the column in points

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Load the image into a memory stream and place it into the cell
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                Image img = new Image
                {
                    ImageStream = imgStream, // assign the stream directly
                    // Optional: set explicit dimensions
                    FixWidth  = 180,
                    FixHeight = 120
                };

                // Add the image to the cell's paragraph collection
                cell.Paragraphs.Add(img);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted into table cell and saved to '{outputPdfPath}'.");
    }
}