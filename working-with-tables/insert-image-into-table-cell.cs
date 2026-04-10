using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

public class InsertImageIntoTableCell
{
    public static void Main()
    {
        // Sample PNG image bytes (1x1 pixel transparent PNG)
        byte[] pngBytes = new byte[] {
            0x89,0x50,0x4E,0x47,0x0D,0x0A,0x1A,0x0A,
            0x00,0x00,0x00,0x0D,0x49,0x48,0x44,0x52,
            0x00,0x00,0x00,0x01,0x00,0x00,0x00,0x01,
            0x08,0x06,0x00,0x00,0x00,0x1F,0x15,0xC4,
            0x89,0x00,0x00,0x00,0x0A,0x49,0x44,0x41,
            0x54,0x78,0x9C,0x63,0x60,0x00,0x00,0x00,
            0x02,0x00,0x01,0xE2,0x21,0xBC,0x33,0x00,
            0x00,0x00,0x00,0x49,0x45,0x4E,0x44,0xAE,
            0x42,0x60,0x82 };

        using (MemoryStream imageStream = new MemoryStream(pngBytes))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                Table table = new Table();
                table.ColumnWidths = "200 200";
                Row row = table.Rows.Add();

                // First cell with text
                Cell textCell = row.Cells.Add("Hello");

                // Second cell with image from memory stream
                Cell imageCell = row.Cells.Add();
                Image image = new Image();
                image.ImageStream = imageStream;
                // Optional scaling of the image inside the cell
                image.FixWidth = 100;
                image.FixHeight = 100;
                imageCell.Paragraphs.Add(image);

                page.Paragraphs.Add(table);

                string outputPath = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Console.WriteLine("Skipping save on macOS due to missing libgdiplus.");
                }
                else
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to {outputPath}");
                }
            }
        }
    }
}