using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization;

class Program
{
    static void Main()
    {
        // Input folder containing PDFs to be sanitized
        const string inputDirectory = "input_pdfs";

        // Output folder where sanitized PDFs will be saved
        const string outputDirectory = "sanitized_pdfs";

        // Verify input folder exists
        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDirectory}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(outputDirectory, fileName);

            try
            {
                // Load the PDF document (lifecycle rule: use Document constructor)
                using (Document doc = new Document(sourcePath))
                {
                    // Remove document metadata
                    doc.RemoveMetadata();

                    // Flatten form fields and annotations
                    doc.Flatten();

                    // Optimize resources (remove unused objects, merge duplicates)
                    doc.OptimizeResources();

                    // Save the sanitized PDF (lifecycle rule: use Save method)
                    doc.Save(destinationPath);
                }

                Console.WriteLine($"Sanitized: {fileName}");
            }
            catch (SanitizationException ex)
            {
                // Specific exception for sanitization failures
                Console.Error.WriteLine($"Sanitization failed for {fileName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General error handling
                Console.Error.WriteLine($"Error processing {fileName}: {ex.Message}");
            }
        }
    }
}