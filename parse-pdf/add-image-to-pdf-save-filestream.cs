using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "image.png";

        // Verify that required files exist
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

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                doc.Pages.Add();
            }

            // Add an image to the first page using the core Aspose.Pdf.Image class
            Aspose.Pdf.Image img = new Aspose.Pdf.Image();
            img.File = imagePath;                     // Set the image source file
            img.FixWidth = 200;                       // Optional: set display width
            img.FixHeight = 150;                      // Optional: set display height

            // Add the image to the page's paragraph collection
            doc.Pages[1].Paragraphs.Add(img);

            // Save the modified PDF to a file via a FileStream and close the stream afterwards
            using (FileStream outStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
            {
                doc.Save(outStream);
            } // outStream is disposed here

            // Alternatively, you could save directly to a path:
            // doc.Save(outputPdfPath);
        } // Document is disposed here

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }
}