using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the image that will be embedded
        const string imagePath = "sample.png";
        // Output PDF portfolio file
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document (will become a portfolio after embedding files)
        using (Document pdf = new Document())
        {
            // Optional: add a blank page so the PDF is not empty
            pdf.Pages.Add();

            // Create a FileSpecification from the image stream.
            // The second argument defines the name that appears in the portfolio.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                FileSpecification fileSpec = new FileSpecification(imgStream, Path.GetFileName(imagePath));
                // Optional: set a description (display name) for the embedded file
                fileSpec.Description = "Sample Image";

                // Add the file specification to the document's embedded files collection
                pdf.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF; the embedded file becomes part of the PDF portfolio
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}