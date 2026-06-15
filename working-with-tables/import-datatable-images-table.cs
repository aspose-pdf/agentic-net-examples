using System;
using System.Data;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace AsposePdfExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create sample images
            string imagePath1 = "sample1.png";
            string imagePath2 = "sample2.png";
            CreateSampleImage(imagePath1, System.Drawing.Color.LightBlue);
            CreateSampleImage(imagePath2, System.Drawing.Color.LightGreen);

            // Build DataTable with image paths
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("PhotoPath", typeof(string));
            DataRow row1 = dataTable.NewRow();
            row1["Name"] = "First";
            row1["PhotoPath"] = imagePath1;
            dataTable.Rows.Add(row1);
            DataRow row2 = dataTable.NewRow();
            row2["Name"] = "Second";
            row2["PhotoPath"] = imagePath2;
            dataTable.Rows.Add(row2);

            // Create PDF document
            using (Document pdfDocument = new Document())
            {
                // Add a page
                Page page = pdfDocument.Pages.Add();

                // Create a table with two columns
                Table table = new Table();
                table.ColumnWidths = "150 150";

                // Import the DataTable (both columns as text initially)
                table.ImportDataTable(dataTable, true, 0, 0);

                // Replace the second column cells with images
                for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++)
                {
                    // Skip header row (rowIndex == 0) if needed
                    if (rowIndex == 0)
                    {
                        // Header row: keep text for both columns
                        continue;
                    }

                    // Get the image path from the original DataTable
                    string imagePath = dataTable.Rows[rowIndex - 1]["PhotoPath"].ToString();

                    // Get the cell that should contain the image (second column, index 1)
                    Aspose.Pdf.Cell imageCell = table.Rows[rowIndex].Cells[1];

                    // Clear any existing paragraphs
                    imageCell.Paragraphs.Clear();

                    // Create an Aspose.Pdf.Image and set its file
                    Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                    pdfImage.File = imagePath;
                    pdfImage.FixWidth = 100f;
                    pdfImage.FixHeight = 100f;

                    // Add the image to the cell
                    imageCell.Paragraphs.Add(pdfImage);
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the PDF
                pdfDocument.Save("output.pdf");
            }
        }

        private static void CreateSampleImage(string filePath, System.Drawing.Color backColor)
        {
            using (Bitmap bitmap = new Bitmap(100, 100))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(backColor);
                }
                bitmap.Save(filePath);
            }
        }
    }
}
