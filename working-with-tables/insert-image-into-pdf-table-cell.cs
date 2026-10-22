using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the image into a memory stream
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        {
            // Open the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the document has at least one page
                Page page = doc.Pages[1]; // 1‑based indexing

                // Create a table with a single column that spans the page width
                Table table = new Table
                {
                    ColumnWidths = "100%" // full page width
                };

                // Add a row and a cell
                Row row = table.Rows.Add();
                Cell cell = row.Cells.Add();

                // Create an Image object from the memory stream
                Image img = new Image
                {
                    ImageStream = imgStream,
                    // Optional: set explicit dimensions
                    FixWidth = 200,
                    FixHeight = 150
                };

                // Insert the image into the cell
                cell.Paragraphs.Add(img);

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}