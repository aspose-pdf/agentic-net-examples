using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "picture.jpg";
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and turn it into a portfolio by using the Collection API
        using (Document doc = new Document())
        {
            // Ensure the document has a Collection object (required for portfolios)
            if (doc.Collection == null)
                doc.Collection = new Collection();

            // Build a FileSpecification for the image file
            var fileSpec = new FileSpecification(imagePath, "SampleImage.jpg")
            {
                Description = "An example image embedded in the PDF portfolio",
                // Provide the file bytes – this is what will be stored in the portfolio
                Contents = new MemoryStream(File.ReadAllBytes(imagePath))
            };

            // Add the file specification to the portfolio collection
            doc.Collection.Add(fileSpec);

            // Save the resulting PDF portfolio
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}
