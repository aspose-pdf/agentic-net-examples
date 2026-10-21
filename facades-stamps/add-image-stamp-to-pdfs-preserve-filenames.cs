using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as required

class Program
{
    static void Main()
    {
        // Input directory containing original PDFs
        const string inputDirectory = @"C:\InputPdfs";
        // Output directory where stamped PDFs will be saved
        const string outputDirectory = @"C:\StampedPdfs";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\Stamp\stamp.png";

        // Validate paths
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputDirectory, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Configure the image stamp
                    ImageStamp stamp = new ImageStamp(stampImagePath)
                    {
                        // Position the stamp at the bottom‑right corner of each page
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment   = VerticalAlignment.Bottom,
                        // Make the stamp semi‑transparent
                        Opacity = 0.5,
                        // Do not place the stamp behind page content
                        Background = false
                    };

                    // Apply the stamp to every page individually
                    foreach (Page page in doc.Pages)
                    {
                        page.AddStamp(stamp);
                    }

                    // Save the modified PDF to the output directory, preserving the original file name
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Stamped PDF saved: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }
    }
}