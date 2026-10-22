using System;
using System.Data;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Prepare a DataTable where each row contains image data as a byte array.
        DataTable imageTable = new DataTable();
        imageTable.Columns.Add("ImageData", typeof(byte[]));

        // For demonstration, load a few image files from disk into the table.
        // Replace the file paths with your actual image locations.
        string[] imageFiles = { "image1.jpg", "image2.png", "image3.bmp" };
        foreach (string filePath in imageFiles)
        {
            if (!File.Exists(filePath))
                continue; // Skip missing files.

            byte[] bytes = File.ReadAllBytes(filePath);
            imageTable.Rows.Add(bytes);
        }

        // Create a new PDF document.
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            // Add a single page to host the table.
            Aspose.Pdf.Page page = pdfDoc.Pages.Add();

            // Create a table with a single column (one image per row).
            Aspose.Pdf.Table table = new Aspose.Pdf.Table();
            // Set column width to 100% of the page width.
            table.ColumnWidths = "100%";

            // Iterate over the DataTable rows and populate the PDF table.
            foreach (DataRow dr in imageTable.Rows)
            {
                // Add a new row to the PDF table.
                Aspose.Pdf.Row pdfRow = table.Rows.Add();

                // Add a cell to the row.
                Aspose.Pdf.Cell pdfCell = pdfRow.Cells.Add();

                // Create an Aspose.Pdf.Image instance.
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();

                // Load the image bytes into the Image via ImageStream.
                byte[] imgBytes = (byte[])dr["ImageData"];
                using (MemoryStream ms = new MemoryStream(imgBytes))
                {
                    pdfImage.ImageStream = ms;
                    // Optionally, set image scaling or margins here.
                    pdfImage.FixWidth = 200; // example fixed width
                }

                // Add the image to the cell's paragraph collection.
                pdfCell.Paragraphs.Add(pdfImage);
            }

            // Add the populated table to the page.
            page.Paragraphs.Add(table);

            // Save the resulting PDF.
            pdfDoc.Save("output.pdf");
        }

        Console.WriteLine("PDF with images generated: output.pdf");
    }
}