using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // existing PDF to turn into a portfolio
        const string imagePath = "picture.jpg";      // image to embed
        const string outputPdfPath = "portfolio_output.pdf";

        // Verify files exist
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has a collection (portfolio) initialized
            doc.Collection ??= new Collection();

            // Create a file specification for the image and embed its contents
            var fileSpec = new FileSpecification(imagePath, "Image attachment")
            {
                Contents = new MemoryStream(File.ReadAllBytes(imagePath)),
                Description = "Sample Image"
            };
            // Set a custom display name (the name shown in the portfolio)
            fileSpec.Name = "Sample Image.jpg";

            // Add the file specification to the document's collection (portfolio)
            doc.Collection.Add(fileSpec);

            // Save the resulting PDF portfolio
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdfPath}");
    }
}
