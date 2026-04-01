using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputDirectory = "input-pdfs";
        const string stampImagePath = "stamp.png";

        // Ensure the input directory exists.
        Directory.CreateDirectory(inputDirectory);

        // Process each PDF file in the directory.
        foreach (string pdfFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string outputFileName = Path.GetFileNameWithoutExtension(pdfFilePath) + "_stamped.pdf";

            // Initialize the facade for stamping.
            PdfFileStamp fileStamp = new PdfFileStamp();
            fileStamp.BindPdf(pdfFilePath);

            // Create and configure the image stamp.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindImage(stampImagePath);
            stamp.Rotation = 90f; // Rotate 90 degrees.
            stamp.SetOrigin(100f, 100f); // Position on the page.
            stamp.SetImageSize(200f, 200f); // Size of the stamp image.

            // Apply the stamp to the first page of the PDF.
            fileStamp.AddStamp(stamp);
            fileStamp.Save(outputFileName);
            fileStamp.Close();

            Console.WriteLine($"Stamped PDF saved as '{outputFileName}'.");
        }

        Console.WriteLine("Batch stamping completed.");
    }
}