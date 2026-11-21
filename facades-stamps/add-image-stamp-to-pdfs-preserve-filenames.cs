using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Only needed if other Facades are used elsewhere; can be removed if not required

class Program
{
    static void Main()
    {
        // Source directory containing the original PDFs
        const string sourceDir = @"C:\InputPdfs";
        // Destination directory where stamped PDFs will be saved
        const string outputDir = @"C:\StampedPdfs";
        // Path to the image that will be used as a stamp
        const string stampImagePath = @"C:\stamp.png";

        // Validate paths
        if (!Directory.Exists(sourceDir))
        {
            Console.Error.WriteLine($"Source directory not found: {sourceDir}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Prepare the stamp (same configuration will be applied to every page)
        ImageStamp stamp = new ImageStamp(stampImagePath)
        {
            // Position the stamp at the bottom‑right corner of each page
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment   = VerticalAlignment.Bottom,
            // Make the stamp semi‑transparent
            Opacity = 0.5,
            // Do not place the stamp behind the page content
            Background = false
        };

        // Process each PDF file in the source directory
        foreach (string filePath in Directory.GetFiles(sourceDir, "*.pdf"))
        {
            string fileName   = Path.GetFileName(filePath);
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF using the high‑level Document API (cross‑platform and fully supported)
                using (Document doc = new Document(filePath))
                {
                    // Apply the stamp to every page
                    foreach (Page page in doc.Pages)
                    {
                        page.AddStamp(stamp);
                    }

                    // Save the modified PDF to the output directory, preserving the original name
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
