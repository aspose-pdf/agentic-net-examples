using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA..."; // replace with actual Base64 string

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Decode the Base64 image to a byte array
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Create an ImageStamp from the image stream
            ImageStamp imageStamp = new ImageStamp(imageStream);

            // Convert 50 mm to points (1 mm = 2.834645669 points)
            double mmToPoints = 2.834645669;
            double offset = 50.0 * mmToPoints;

            // Position the stamp 50 mm from the left and bottom edges
            imageStamp.XIndent = offset;
            imageStamp.YIndent = offset;

            using (Document doc = new Document(inputPath))
            {
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The document does not contain a third page.");
                    return;
                }

                Page page = doc.Pages[3]; // Pages are 1‑based
                page.AddStamp(imageStamp);
                doc.Save(outputPath);
                Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPath}'.");
            }
        }
    }
}