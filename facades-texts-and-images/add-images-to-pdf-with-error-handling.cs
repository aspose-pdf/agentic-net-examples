using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Example list of image files to add
        string[] imageFiles = { "image1.jpg", "image2.png", "missing.jpg" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the high‑level Document API (PdfContentEditor does not expose AddImage)
        Document pdf = new Document(inputPdf);
        int targetPage = 1; // add images to the first page (adjust as needed)

        foreach (string imgPath in imageFiles)
        {
            try
            {
                // ImageStamp will throw if the file is missing or unsupported
                ImageStamp stamp = new ImageStamp(imgPath);
                // Optional: set position/size – here we use default placement
                pdf.Pages[targetPage].AddStamp(stamp);
            }
            catch (Exception ex)
            {
                // Log the problematic image file path
                Console.Error.WriteLine($"Error adding image '{imgPath}': {ex.Message}");
            }
        }

        // Save the modified PDF
        pdf.Save(outputPdf);

        Console.WriteLine($"Processing finished. Output saved to '{outputPdf}'.");
    }
}
