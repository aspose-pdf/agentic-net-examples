using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the image file (used as a memory stream), and the output PDF.
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "sample.jpg";
        const string outputPdfPath = "output.pdf";

        // Verify that the required files exist.
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

        // Open the source PDF inside a using block to ensure proper disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the image into a memory stream.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Create an Image object and assign the stream.
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    // The ImageStream property accepts a Stream containing the image data.
                    ImageStream = imgStream
                };

                // Create a table with a single cell.
                Aspose.Pdf.Table table = new Aspose.Pdf.Table();

                // Optionally set column widths (here we use a single column that spans the page width).
                table.ColumnWidths = "100%";

                // Add a row to the table.
                Aspose.Pdf.Row row = table.Rows.Add();

                // Add a cell to the row.
                Aspose.Pdf.Cell cell = row.Cells.Add();

                // Insert the image into the cell's paragraph collection.
                cell.Paragraphs.Add(pdfImage);

                // Add the table to the first page of the document.
                // Ensure the page exists; Aspose.Pdf uses 1‑based indexing.
                if (pdfDoc.Pages.Count == 0)
                {
                    pdfDoc.Pages.Add();
                }
                pdfDoc.Pages[1].Paragraphs.Add(table);
            }

            // Save the modified PDF. The using block ensures the document is disposed after saving.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted into table cell and saved to '{outputPdfPath}'.");
    }
}