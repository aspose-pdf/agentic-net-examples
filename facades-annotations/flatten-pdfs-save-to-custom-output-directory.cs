using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as required

class Program
{
    static void Main()
    {
        // Input directory containing PDFs to be flattened
        const string inputDirectory = @"C:\InputPdfs";
        // Custom output directory where flattened PDFs will be saved
        const string outputDirectory = @"C:\FlattenedPdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the input directory
        foreach (string inputFilePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            // Derive the output file path preserving the original file name
            string fileName = Path.GetFileName(inputFilePath);
            string outputFilePath = Path.Combine(outputDirectory, fileName);

            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(inputFilePath))
                {
                    // Flatten the document (remove form fields and replace with their values)
                    pdfDoc.Flatten();

                    // Save the flattened PDF to the custom output directory
                    pdfDoc.Save(outputFilePath);
                }

                Console.WriteLine($"Flattened PDF saved to: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputFilePath}': {ex.Message}");
            }
        }
    }
}