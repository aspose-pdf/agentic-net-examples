using System;
using System.IO;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Determine the input PDF path – either from the first command‑line argument or the default "sample.pdf"
            string inputPath = args.Length > 0 ? args[0] : "sample.pdf";
            string outputPath = "cropped.pdf";

            // Verify that the source file exists before attempting to load it
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: The file '{inputPath}' was not found. Please provide a valid PDF file path.");
                return;
            }

            // Load the PDF document
            using (Document document = new Document(inputPath))
            {
                // Define a crop rectangle (left, bottom, right, top)
                Rectangle cropRect = new Rectangle(50, 50, 500, 800);

                // Apply the crop box to the first page
                document.Pages[1].CropBox = cropRect;

                // Retrieve the TrimBox after cropping – TrimBox reflects the visible area of the page
                Rectangle trimBox = document.Pages[1].TrimBox;

                // Output TrimBox coordinates
                Console.WriteLine("TrimBox: LLX={0}, LLY={1}, URX={2}, URY={3}",
                    trimBox.LLX, trimBox.LLY, trimBox.URX, trimBox.URY);

                // Save the modified PDF (optional)
                document.Save(outputPath);
                Console.WriteLine($"Cropped PDF saved to '{outputPath}'.");
            }
        }
    }
}
