using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class InsertImages
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_images.pdf";
        const string imagePath = "picture.jpg";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use PdfFileMend facade to add images without recreating the whole document
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF
            mend.BindPdf(inputPdf);

            // Add the image to page 1.
            // Coordinates are in points (1/72 inch). Adjust as required.
            // lowerLeftX, lowerLeftY, upperRightX, upperRightY define the rectangle.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Preserve original image quality by using the stream directly.
                // Example places the image at (50, 500) with size 200x150 points.
                mend.AddImage(imgStream, 1, 50f, 500f, 250f, 650f);
            }

            // If you need to add the same image to additional pages, repeat AddImage
            // with the desired page numbers and coordinates.

            // Save the modified PDF
            mend.Save(outputPdf);
            mend.Close();
        }

        Console.WriteLine($"Images inserted successfully. Output saved to '{outputPdf}'.");
    }
}