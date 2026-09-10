using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Optionally crop the first page (example values)
            // Rectangle constructor: left, bottom, width, height
            Aspose.Pdf.Rectangle newCrop = new Aspose.Pdf.Rectangle(50, 50, 500, 700);
            doc.Pages[1].CropBox = newCrop;

            // Retrieve the TrimBox after cropping
            Aspose.Pdf.Rectangle trimBox = doc.Pages[1].TrimBox;

            // Output TrimBox coordinates
            Console.WriteLine($"TrimBox: LLX={trimBox.LLX}, LLY={trimBox.LLY}, URX={trimBox.URX}, URY={trimBox.URY}");
        }
    }
}