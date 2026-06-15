using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // existing PDF
        const string outputPdfPath = "output.pdf";     // result PDF
        const string imagePath     = "picture.png";    // image file to embed

        // Ensure the source files exist
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

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Add a new page if the document has no pages
            if (pdfDoc.Pages.Count == 0)
                pdfDoc.Pages.Add();

            // Use the first page for demonstration
            Page page = pdfDoc.Pages[1];

            // Create a table and add it to the page
            Table table = new Table
            {
                // Define two equal-width columns (optional)
                ColumnWidths = "200 200",
                // Optional border for visual reference
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };
            page.Paragraphs.Add(table);

            // Add a row
            Row row = table.Rows.Add();

            // Add a cell where the image will be placed
            Cell imageCell = row.Cells.Add();

            // Load the image into a memory stream
            using (FileStream imgFileStream = File.OpenRead(imagePath))
            using (MemoryStream imgMemoryStream = new MemoryStream())
            {
                imgFileStream.CopyTo(imgMemoryStream);
                imgMemoryStream.Position = 0; // reset stream position

                // Create an Image object and assign the stream
                Image pdfImage = new Image
                {
                    ImageStream = imgMemoryStream,
                    // Optional scaling – fit the image within the cell
                    ImageScale = 0.5f
                };

                // Add the image to the cell's paragraph collection
                imageCell.Paragraphs.Add(pdfImage);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted and PDF saved to '{outputPdfPath}'.");
    }
}