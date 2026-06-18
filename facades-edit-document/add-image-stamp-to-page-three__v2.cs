using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Base64‑encoded image data (replace with actual string)
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA...";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Decode the Base64 string into a byte array and wrap it in a MemoryStream
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Initialise the PdfFileStamp facade using the modern API
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(inputPdfPath); // replaces obsolete InputFile property

            // Create a stamp, bind the image stream and configure its placement
            Stamp stamp = new Stamp();
            stamp.BindImage(imageStream);               // use the image as stamp content
            stamp.Pages = new int[] { 3 };              // apply only to page 3

            // Convert 50 mm to points (1 pt = 1/72 in, 1 mm ≈ 2.83465 pt)
            const float mmToPoints = 72f / 25.4f;        // float literals because SetOrigin expects float
            float position = 50f * mmToPoints;          // 50 mm in points as float

            stamp.SetOrigin(position, position);        // X and Y coordinates from bottom‑left (float overload)

            // Add the configured stamp to the document and save
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputPdfPath); // replaces obsolete OutputFile property
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPdfPath}'.");
    }
}
