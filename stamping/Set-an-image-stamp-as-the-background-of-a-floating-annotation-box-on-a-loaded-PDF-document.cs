using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as required

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stampImage.jpg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document using the recommended lifecycle pattern
        using (Document doc = new Document(inputPdf))
        {
            // Create a FloatingBox with desired size
            float boxWidth  = 200f;
            float boxHeight = 100f;
            FloatingBox floatingBox = new FloatingBox(boxWidth, boxHeight);

            // Set the background image of the FloatingBox
            floatingBox.BackgroundImage = new Image { File = imagePath };

            // Optional: set position of the FloatingBox on the page
            floatingBox.Left = 100f; // X coordinate from the left
            floatingBox.Top  = 500f; // Y coordinate from the bottom

            // Optional: set a border for visual reference
            floatingBox.Border = new BorderInfo(BorderSide.All, Aspose.Pdf.Color.Black);

            // Add the FloatingBox to the first page's paragraph collection
            Page firstPage = doc.Pages[1];
            firstPage.Paragraphs.Add(floatingBox);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with image stamp background: {outputPdf}");
    }
}