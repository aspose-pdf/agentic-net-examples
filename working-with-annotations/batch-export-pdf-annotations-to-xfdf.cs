using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDF files (relative to the executable directory)
        const string inputFolder = "InputPdfs";
        // Folder where the XFDF files will be saved (relative to the executable directory)
        const string outputFolder = "OutputXfdf";

        // Resolve full paths based on the current working directory to avoid path issues
        string inputPath = Path.GetFullPath(inputFolder);
        string outputPath = Path.GetFullPath(outputFolder);

        // Ensure the output directory exists
        Directory.CreateDirectory(outputPath);

        // Verify that the input directory exists; if not, inform the user and exit gracefully
        if (!Directory.Exists(inputPath))
        {
            Console.WriteLine($"Input folder '{inputPath}' does not exist. Please create the folder and place PDF files inside it before running the program.");
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfFile in Directory.GetFiles(inputPath, "*.pdf"))
        {
            // Derive the XFDF file name from the PDF file name
            string baseName = Path.GetFileNameWithoutExtension(pdfFile);
            string xfdfFile = Path.Combine(outputPath, baseName + ".xfdf");

            try
            {
                // Load the PDF and export its annotations to XFDF
                using (Document doc = new Document(pdfFile))
                {
                    doc.ExportAnnotationsToXfdf(xfdfFile);
                }

                Console.WriteLine($"Exported annotations from '{pdfFile}' to '{xfdfFile}'.");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors but continue processing remaining files
                Console.WriteLine($"Failed to export annotations from '{pdfFile}': {ex.Message}");
            }
        }
    }
}
