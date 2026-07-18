using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the image to embed and the output PDF portfolio
        const string imagePath   = "image.png";
        const string outputPath  = "portfolio.pdf";
        const string displayName = "MyImage.png"; // Name shown in the portfolio

        // Ensure the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document (will become a portfolio after embedding files)
        using (Document doc = new Document())
        {
            // Create a FileSpecification for the image.
            // The constructor takes a stream containing the file data and a display name.
            using (FileStream imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                FileSpecification fileSpec = new FileSpecification(imgStream, displayName);

                // Add the file to the EmbeddedFiles collection.
                // Using the overload with a key sets the display name shown in the portfolio.
                doc.EmbeddedFiles.Add(displayName, fileSpec);
            }

            // Save the PDF portfolio.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF portfolio created: {outputPath}");
    }
}