using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "logo.png";

        // Verify that the source PDF and image exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page to which the stamp will be applied (first page in this example)
            Page page = doc.Pages[1];

            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set the position of the stamp on the page (coordinates are from the lower‑left corner)
            imgStamp.XIndent = 100; // horizontal offset
            imgStamp.YIndent = 100; // vertical offset

            // Optionally set the size of the stamp
            imgStamp.Width  = 200;
            imgStamp.Height = 100;

            // Rotate the stamp 180 degrees so it appears upside‑down
            imgStamp.Rotate = Rotation.on180;   // multiples of 90° are supported
            // Alternatively, for arbitrary angles: imgStamp.RotateAngle = 180;

            // Add the configured stamp to the selected page
            page.AddStamp(imgStamp);

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}
