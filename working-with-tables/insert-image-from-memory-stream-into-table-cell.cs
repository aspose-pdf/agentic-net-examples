using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "output_with_image_in_table.pdf";

        // Create a simple bitmap in memory (no external file required)
        using (var bitmap = new System.Drawing.Bitmap(100, 100))
        {
            using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                graphics.Clear(System.Drawing.Color.LightBlue);
                graphics.DrawEllipse(System.Drawing.Pens.DarkBlue, 10, 10, 80, 80);
            }

            // Save the bitmap to a memory stream as PNG
            using (var imageStream = new MemoryStream())
            {
                bitmap.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);
                imageStream.Position = 0; // rewind for reading

                // Build the PDF
                using (var doc = new Aspose.Pdf.Document())
                {
                    var page = doc.Pages.Add();

                    var table = new Aspose.Pdf.Table { ColumnWidths = "100%" };
                    var row = table.Rows.Add();
                    var cell = row.Cells.Add();

                    // Insert the image from the memory stream into the cell
                    var img = new Aspose.Pdf.Image { ImageStream = imageStream };
                    // Optional scaling:
                    // img.FixWidth = 200;
                    // img.FixHeight = 150;

                    cell.Paragraphs.Add(img);
                    page.Paragraphs.Add(table);

                    doc.Save(outputPdf);
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
