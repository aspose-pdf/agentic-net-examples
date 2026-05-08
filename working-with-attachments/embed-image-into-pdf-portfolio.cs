using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "portfolio.pdf";
        const string imageFile = "image.png";

        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Create a new PDF document (will become a portfolio when files are embedded)
        using (Document doc = new Document())
        {
            // Add a blank page (required for a valid PDF)
            doc.Pages.Add();

            // Create a file specification for the image to embed using the constructor
            // The first argument is the file path, the second is a description.
            FileSpecification fileSpec = new FileSpecification(imageFile, "Embedded image file");
            // Set the display name that will appear in the portfolio
            fileSpec.Name = "SampleImage.png";

            // Add the file specification to the embedded files collection.
            // The first argument is a unique key for the embedded file.
            doc.EmbeddedFiles.Add("image1", fileSpec);

            // Save the PDF portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}
