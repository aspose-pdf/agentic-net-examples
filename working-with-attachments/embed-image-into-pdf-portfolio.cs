using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "portfolio.pdf";
        const string imagePath = "sample.jpg";
        const string displayName = "MyImage.jpg";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document (will become a portfolio when files are embedded)
        using (Document doc = new Document())
        {
            // Add a blank page – optional, a portfolio can exist without visible pages
            doc.Pages.Add();

            // Create a file specification for the image to embed (description is optional)
            FileSpecification fileSpec = new FileSpecification(imagePath, "Embedded image for portfolio");
            // Set the name that will appear in the attachment list
            fileSpec.Name = displayName;

            // Add the file specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the document; presence of embedded files makes it a PDF Portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}