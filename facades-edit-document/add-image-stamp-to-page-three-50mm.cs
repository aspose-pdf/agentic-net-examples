using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Base64 string representing the image (replace with actual data)
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA...";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Decode the Base64 string into a byte array
        byte[] imageBytes = Convert.FromBase64String(base64Image);

        // Create a memory stream from the image bytes
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        {
            // Create an ImageStamp from the stream
            Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(imgStream);

            // Convert 50 mm to points (1 point = 1/72 inch, 1 inch = 25.4 mm)
            double mm = 50.0;
            double points = mm * 72.0 / 25.4; // ≈141.73 points

            // Position the stamp 50 mm from the left and bottom edges
            imgStamp.XIndent = points;
            imgStamp.YIndent = points;

            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Ensure the document has at least three pages
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The document contains fewer than 3 pages.");
                    return;
                }

                // Add the stamp to page three
                doc.Pages[3].AddStamp(imgStamp);

                // Save the modified PDF
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPath}'.");
    }
}