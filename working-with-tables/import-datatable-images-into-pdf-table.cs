using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a DataTable with a text column and an image column (byte[]).
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Photo", typeof(byte[]));

        // Load an example image from disk into a byte array.
        const string imagePath = "sample.jpg";
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        byte[] imageBytes = File.ReadAllBytes(imagePath);
        dt.Rows.Add("Sample Item", imageBytes);
        // Add more rows as needed...

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table with two columns.
            Table table = new Table();
            // Set column widths (example: 150 points for text, 200 points for image).
            table.ColumnWidths = "150 200";

            // Optional: set table border for visibility using the correct BorderInfo class.
            table.Border = new BorderInfo(BorderSide.All, 1);

            // Iterate through the DataTable rows and populate the table.
            foreach (DataRow dr in dt.Rows)
            {
                // Add a new row to the table.
                Aspose.Pdf.Row row = table.Rows.Add();

                // First cell: text.
                Aspose.Pdf.Cell textCell = row.Cells.Add();
                string name = dr["Name"]?.ToString() ?? string.Empty;
                TextFragment tf = new TextFragment(name);
                textCell.Paragraphs.Add(tf);

                // Second cell: image.
                Aspose.Pdf.Cell imageCell = row.Cells.Add();
                byte[] imgData = dr["Photo"] as byte[];
                if (imgData != null && imgData.Length > 0)
                {
                    // Aspose.Pdf.Image must be created with the parameter‑less constructor
                    // and the image data supplied via the ImageStream property.
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        Image img = new Image { ImageStream = ms };
                        // Optionally set scaling, margins, etc.
                        imageCell.Paragraphs.Add(img);
                    }
                }
            }

            // Add the populated table to the page.
            page.Paragraphs.Add(table);

            // Save the PDF.
            const string outputPath = "output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
