using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where processed PDFs will be saved
        const string outputFolder = "OutputPdfs";
        // XFDF file that holds the annotation data to be applied to each PDF
        const string xfdfPath = "annotations.xfdf";

        // Validate XFDF file existence
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Gather all PDF file paths from the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        // Process each PDF in parallel for efficiency
        Parallel.ForEach(pdfFiles, pdfPath =>
        {
            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    // Import annotations from the XFDF file into the current document
                    doc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Determine the output file path (same name, different folder)
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

                    // Save the modified PDF; Save() writes a PDF regardless of extension
                    doc.Save(outputPath);

                    Console.WriteLine($"Processed: {pdfPath} → {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing an individual PDF
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}