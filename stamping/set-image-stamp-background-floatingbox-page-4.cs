using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            // Get page 4 (1‑based indexing)
            Page pageFour = doc.Pages[4];

            // Create a FloatingBox (size can be adjusted as needed)
            FloatingBox floatingBox = new FloatingBox(500, 700); // width, height

            // Set the background image for the FloatingBox
            Image bgImage = new Image();
            bgImage.File = stampImage;          // path to the image file
            floatingBox.BackgroundImage = bgImage;

            // Optionally set background color or other properties
            // floatingBox.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add the FloatingBox to the page's paragraph collection
            pageFour.Paragraphs.Add(floatingBox);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with image stamp in FloatingBox on page 4: {outputPdf}");
    }
}