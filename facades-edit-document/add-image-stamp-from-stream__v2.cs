using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

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

        using (FileStream imgStream = File.OpenRead(imagePath))
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an image stamp from the memory stream
            ImageStamp imgStamp = new ImageStamp(imgStream);
            // Scale the stamp to 50% of its original size
            imgStamp.Zoom = 0.5f;

            // Add the stamp to the first page of the PDF
            Page firstPage = pdfDoc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}