using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 4 exists
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            // Create an Image object for the background stamp
            Image backgroundImg = new Image();
            backgroundImg.File = stampImagePath;

            // Create a FloatingBox and assign the background image
            FloatingBox floatingBox = new FloatingBox();
            floatingBox.BackgroundImage = backgroundImg;

            // Add the FloatingBox to page 4
            Page pageFour = doc.Pages[4];
            pageFour.Paragraphs.Add(floatingBox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with image stamp as background in FloatingBox on page 4: {outputPath}");
    }
}