using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "image.jpg";
        const string outputPdf = "output.pdf";

        // Verify that source files exist
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

        // Use the PdfFileMend facade to modify the PDF
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdf);

            // Insert the image on page 1.
            // Coordinates are in default PDF units (points).
            // lower-left (10,10) and upper-right (200,200) define the rectangle.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                bool success = mend.AddImage(imgStream, 1, 10f, 10f, 200f, 200f);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to add the image to the PDF.");
                }
            }

            // Save the modified PDF to a new file
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}