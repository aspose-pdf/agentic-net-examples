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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Load the image into a memory stream
            using (FileStream imgFile = File.OpenRead(imagePath))
            using (MemoryStream imgStream = new MemoryStream())
            {
                imgFile.CopyTo(imgStream);
                imgStream.Position = 0; // reset stream position

                // Create an ImageStamp from the memory stream
                ImageStamp imgStamp = new ImageStamp(imgStream)
                {
                    Background = false,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Opacity = 0.5f
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}