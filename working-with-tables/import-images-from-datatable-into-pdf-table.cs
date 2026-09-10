using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;   // for Image class

class Program
{
    static void Main()
    {
        // Prepare a DataTable that holds image data (as byte arrays)
        DataTable imageTable = new DataTable();
        imageTable.Columns.Add("ImageData", typeof(byte[]));

        // Example: load two sample images from disk into the table
        // (Replace the paths with actual image files on your system)
        string[] sampleImages = { "image1.png", "image2.jpg" };
        foreach (string imgPath in sampleImages)
        {
            if (!File.Exists(imgPath))
                continue; // skip missing files

            byte[] bytes = File.ReadAllBytes(imgPath);
            DataRow row = imageTable.NewRow();
            row["ImageData"] = bytes;
            imageTable.Rows.Add(row);
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page
            pdfDoc.Pages.Add();

            // Create a table that will hold the images
            Table table = new Table
            {
                // Define column widths (adjust as needed)
                ColumnWidths = "200 200"
            };

            // Iterate through the DataTable rows and add each image to a new table row
            foreach (DataRow dr in imageTable.Rows)
            {
                // Add a new row to the table
                var pdfRow = table.Rows.Add();

                // Add a single cell to the row
                var cell = pdfRow.Cells.Add();

                // Create an Image object from the byte array
                Image img = new Image
                {
                    // Use the ImageStream property to supply the image bytes
                    ImageStream = new MemoryStream((byte[])dr["ImageData"])
                };

                // Optionally set scaling or alignment
                img.FixWidth = 180;   // fit within column width
                img.FixHeight = 120;  // maintain reasonable height
                img.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the image to the cell's paragraph collection
                cell.Paragraphs.Add(img);
            }

            // Add the populated table to the first page
            pdfDoc.Pages[1].Paragraphs.Add(table);

            // Save the resulting PDF
            pdfDoc.Save("ImageTable.pdf");
        }

        Console.WriteLine("PDF with image table created successfully.");
    }
}