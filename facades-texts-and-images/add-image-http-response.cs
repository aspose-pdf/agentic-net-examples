using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagePath = "image.png";
        const string outputPdfPath = "output.pdf";

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

        // Load the existing PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Add the image to the first page at a specific location
            using (FileStream imageStream = File.OpenRead(imagePath))
            {
                // Define the rectangle where the image will be placed (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
                pdfDocument.Pages[1].AddImage(imageStream, imageRect);
            }

            // Save the modified PDF into a memory stream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                pdfDocument.Save(memoryStream);

                // Simulate writing the PDF to an HTTP response by saving the stream to a file
                memoryStream.Position = 0;
                using (FileStream outputFileStream = File.OpenWrite(outputPdfPath))
                {
                    memoryStream.CopyTo(outputFileStream);
                }
                Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
            }
        }
    }
}