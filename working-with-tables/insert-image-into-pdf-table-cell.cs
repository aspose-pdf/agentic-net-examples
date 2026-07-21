using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF
        const string outputPdfPath = "output.pdf";     // result PDF
        const string imagePath     = "image.png";      // image to embed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a table with a single column
            Table table = new Table();
            // Define column width (optional)
            table.ColumnWidths = "200";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Load the image from a memory stream and add it to the cell
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                Image img = new Image
                {
                    // Assign the stream containing the image data
                    ImageStream = imgStream
                };
                // Add the image to the cell's paragraph collection
                cell.Paragraphs.Add(img);
            }

            // Insert the table into the first page of the PDF
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted into table cell and saved to '{outputPdfPath}'.");
    }
}