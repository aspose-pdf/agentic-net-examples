using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Verify input files exist
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

        // Open the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Ensure there is at least one page to host the table
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a simple table with one column
            Table table = new Table
            {
                ColumnWidths = "200", // width of the single column
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add a row and a cell
            Row row = table.Rows.Add();
            Cell cell = row.Cells.Add();

            // Create an Image object and point it to the external file
            Image img = new Image
            {
                File = imagePath,
                // Optional: scale the image to fit the cell
                ImageScale = 0.5
            };

            // Insert the image into the cell's paragraph collection
            cell.Paragraphs.Add(img);

            // Add the table to the first page of the document
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted into table cell and saved to '{outputPdf}'.");
    }
}