using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "image.jpg";
        const string outputPdfPath = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Use PdfFileMend facade to add an image to the PDF
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdfPath);

            // Open the image as a stream and add it to page 1
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Coordinates: lower-left (10,10), upper-right (100,100)
                mend.AddImage(imgStream, 1, 10, 10, 100, 100);
            }

            // Save the modified PDF to a new file
            mend.Save(outputPdfPath);

            // Close the facade (optional when using 'using')
            mend.Close();
        }

        Console.WriteLine($"Image inserted successfully. Output saved to '{outputPdfPath}'.");
    }
}