using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes: PdfFileStamp, Stamp

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // destination PDF
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA..."; // replace with actual Base64 string

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Convert Base64 string to a memory stream containing the image data
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        {
            // Create a stamp and bind the image stream to it
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(imgStream);

            // 50 mm → points (1 point = 1/72 inch, 1 inch = 25.4 mm)
            const double mmToPoints = 72.0 / 25.4;
            double xPos = 50 * mmToPoints; // horizontal position
            double yPos = 50 * mmToPoints; // vertical position

            // Set stamp position (origin) on the page
            stamp.SetOrigin((float)xPos, (float)yPos);

            // Specify the target page (Aspose.Pdf uses 1‑based indexing)
            stamp.PageNumber = 3;

            // Apply the stamp using the PdfFileStamp facade
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.InputFile  = inputPdf;
            fileStamp.OutputFile = outputPdf;
            fileStamp.AddStamp(stamp);
            fileStamp.Close(); // Persist changes
        }

        Console.WriteLine($"Image stamp added to page 3 at 50 mm. Saved as '{outputPdf}'.");
    }
}