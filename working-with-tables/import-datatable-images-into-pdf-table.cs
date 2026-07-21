using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Prepare a DataTable where the second column holds image data as byte arrays.
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Photo", typeof(byte[]));

        // Load sample images from disk into the DataTable.
        // Adjust the file paths as needed for your environment.
        string[] imageFiles = { "photo1.png", "photo2.png" };
        string[] names = { "Alice", "Bob" };

        for (int i = 0; i < imageFiles.Length; i++)
        {
            if (File.Exists(imageFiles[i]))
            {
                byte[] imgBytes = File.ReadAllBytes(imageFiles[i]);
                dt.Rows.Add(names[i], imgBytes);
            }
        }

        // Create a new PDF document.
        using (Document doc = new Document())
        {
            // Add a page to host the table.
            Page page = doc.Pages.Add();

            // Create a table with two columns.
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "150 150",
                // Optional: set a border for all cells.
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Add a header row.
            var headerRow = table.Rows.Add();
            headerRow.Cells.Add("Name");
            headerRow.Cells.Add("Photo");

            // Populate the table rows from the DataTable.
            foreach (DataRow dr in dt.Rows)
            {
                var row = table.Rows.Add();

                // Text cell for the name.
                row.Cells.Add(dr["Name"].ToString());

                // Image cell.
                var imgCell = row.Cells.Add();

                // Create an Image object from the byte array.
                using (MemoryStream ms = new MemoryStream((byte[])dr["Photo"]))
                {
                    Image img = new Image
                    {
                        // Assign the stream containing the image data.
                        ImageStream = ms,
                        // Optionally fix the displayed size.
                        FixWidth = 100,
                        FixHeight = 100
                    };

                    // Add the image to the cell's paragraph collection.
                    imgCell.Paragraphs.Add(img);
                }
            }

            // Add the table to the page.
            page.Paragraphs.Add(table);

            // Save the PDF.
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with table and images created successfully.");
    }
}