using System;
using System.IO;
using Aspose.Pdf;

namespace RotatePdfPages
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "input.pdf";
            string outputPath = "rotated.pdf";

            // Verify that the source PDF exists before attempting to load it.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Error: Input file '{inputPath}' not found.");
                return;
            }

            // Load the PDF document.
            using (Document pdfDocument = new Document(inputPath))
            {
                // Rotate every page 180 degrees.
                foreach (Page page in pdfDocument.Pages)
                {
                    page.Rotate = Rotation.on180; // Correct enum member.
                }

                // Save the rotated document.
                pdfDocument.Save(outputPath);
                Console.WriteLine($"All pages rotated 180 degrees and saved to '{outputPath}'.");
            }
        }
    }
}
