using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        // Base64‑encoded image (replace with actual data)
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA...";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Decode the Base64 string to a byte array
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create a stream for the image data
            using (MemoryStream imgStream = new MemoryStream(imageBytes))
            {
                // Create an ImageStamp from the stream
                ImageStamp stamp = new ImageStamp(imgStream);

                // Positioning: 1 mm = 2.834645669 points
                const double mmToPoints = 2.834645669;
                stamp.YIndent = 50 * mmToPoints; // 50 mm from the top edge
                stamp.XIndent = 0;               // left margin (adjust as needed)

                // Alignment – Top ensures YIndent is measured from the top of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Left;
                stamp.VerticalAlignment = VerticalAlignment.Top;

                // Ensure the stamp appears above page content
                stamp.Background = false;

                // Add the stamp to page 3 (pages are 1‑based)
                pdfDoc.Pages[3].AddStamp(stamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
