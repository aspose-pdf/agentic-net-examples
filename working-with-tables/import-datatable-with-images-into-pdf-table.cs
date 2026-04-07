using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a DataTable with text and image data (as byte[])
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Photo", typeof(byte[]));

        // Sample image file – replace with your own image path
        string sampleImagePath = "sample.png";
        if (!File.Exists(sampleImagePath))
        {
            Console.Error.WriteLine($"Image file not found: {sampleImagePath}");
            return;
        }

        byte[] imageBytes = File.ReadAllBytes(sampleImagePath);
        dt.Rows.Add("First Item", imageBytes);
        dt.Rows.Add("Second Item", imageBytes);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the table
            Page page = doc.Pages.Add();

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (adjust as needed)
                ColumnWidths = "200 200"
            };

            // Import the DataTable into the table (including column names)
            // firstFilledRow and firstFilledColumn are zero‑based indexes
            table.ImportDataTable(dt, true, 0, 0);

            // After import, replace the cells that contain image data (byte[])
            // with actual image objects so they render inside the table.
            for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
            {
                // The second column (index 1) holds the image data
                int imageColIndex = 1;
                var cell = table.Rows[rowIndex].Cells[imageColIndex];

                // Retrieve the original byte[] from the DataTable
                byte[] imgData = dt.Rows[rowIndex][imageColIndex] as byte[];
                if (imgData == null) continue;

                // Write the image bytes to a temporary file (Aspose.Pdf.Image expects a file path)
                string tempImgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".png");
                File.WriteAllBytes(tempImgPath, imgData);

                // Clear any placeholder text that ImportDataTable may have inserted
                cell.Paragraphs.Clear();

                // Create an Image object and point it to the temporary file
                Image pdfImage = new Image
                {
                    File = tempImgPath
                };

                // Add the image to the cell's paragraph collection
                cell.Paragraphs.Add(pdfImage);
            }

            // Add the fully prepared table to the page
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with table and images created: output.pdf");
    }
}