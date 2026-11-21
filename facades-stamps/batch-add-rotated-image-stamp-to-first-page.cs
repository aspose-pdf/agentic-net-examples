using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDFs
        const string inputDir = @"C:\PdfInput";
        // Output directory for stamped PDFs
        const string outputDir = @"C:\PdfOutput";
        // Path to the image to be used as a stamp
        const string stampImagePath = @"C:\stamp.png";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Iterate over all PDF files in the input directory
        foreach (string pdfPath in Directory.GetFiles(inputDir, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outputPath = Path.Combine(outputDir, fileName);

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Ensure the document has at least one page
                    if (doc.Pages.Count < 1)
                    {
                        Console.WriteLine($"Skipping empty PDF: {fileName}");
                        continue;
                    }

                    // Create an ImageStamp from the specified image
                    ImageStamp imgStamp = new ImageStamp(stampImagePath)
                    {
                        // Position the stamp (optional)
                        XIndent = 100,
                        YIndent = 100,
                        // Keep the stamp on top of existing content
                        Background = false
                    };
                    // Rotate the stamp by 45 degrees (arbitrary angle)
                    imgStamp.RotateAngle = 45f; // Use RotateAngle instead of Rotate (which expects the Rotation enum)

                    // Add the stamp to the first page (page indexing is 1‑based)
                    Page firstPage = doc.Pages[1];
                    firstPage.AddStamp(imgStamp);

                    // Save the modified PDF to the output directory
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
