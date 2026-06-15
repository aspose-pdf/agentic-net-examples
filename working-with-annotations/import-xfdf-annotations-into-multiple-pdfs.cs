using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing source PDFs
        const string inputDir = "InputPdfs";
        // Directory where annotated PDFs will be saved
        const string outputDir = "OutputPdfs";
        // XFDF file with annotation data to import
        const string xfdfPath = "annotations.xfdf";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Validate XFDF file presence
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Gather all PDF files to process
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");

        // Process PDFs in parallel for efficiency
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Determine output file name
                string fileName = Path.GetFileNameWithoutExtension(pdfPath);
                string outputPath = Path.Combine(outputDir, $"{fileName}_annotated.pdf");

                // Load PDF, import XFDF annotations, and save
                using (Document doc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save the updated PDF
                    doc.Save(outputPath);
                }

                Console.WriteLine($"Successfully processed: {pdfPath} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {pdfPath}: {ex.Message}");
            }
        });
    }
}