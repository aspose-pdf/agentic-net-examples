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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing).
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create a table and add it to the page.
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            page.Paragraphs.Add(table);

            // Add a single row.
            Aspose.Pdf.Row row = table.Rows.Add();

            // Add a single cell to the row.
            Aspose.Pdf.Cell cell = row.Cells.Add();

            // Load the image into a memory stream.
            using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imagePath)))
            {
                // Create an Image object and assign the stream.
                Aspose.Pdf.Image img = new Aspose.Pdf.Image
                {
                    ImageStream = imgStream
                };

                // Add the image to the cell's paragraph collection.
                cell.Paragraphs.Add(img);
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image inserted into table cell and saved to '{outputPdfPath}'.");
    }
}