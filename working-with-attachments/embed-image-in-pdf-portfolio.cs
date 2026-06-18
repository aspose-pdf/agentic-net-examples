using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "sample.png";
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        using (Document pdfDoc = new Document())
        {
            // Create a file specification for the image to be embedded
            FileSpecification fileSpec = new FileSpecification(imagePath)
            {
                // Set the display name that will appear in the portfolio's attachment list
                Name = "MyImage.png",
                Description = "Sample image for portfolio"
            };

            // Add the file specification to the document's embedded files collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF portfolio
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}