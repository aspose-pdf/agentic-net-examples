using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve absolute paths based on the executable location
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string inputFolder = Path.Combine(baseDir, "InputPdfs");
        string outputFolder = Path.Combine(baseDir, "XfdfOutput");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input directory exists – if not, create it and inform the user
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating an empty folder. Place PDF files there and re‑run the program.");
            Directory.CreateDirectory(inputFolder);
            return; // Nothing to process on first run
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Determine the output XFDF file path (same name, .xfdf extension)
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string xfdfPath = Path.Combine(outputFolder, baseName + ".xfdf");

                // Load the PDF and export its annotations to XFDF
                using (Document doc = new Document(pdfPath))
                {
                    doc.ExportAnnotationsToXfdf(xfdfPath);
                }

                Console.WriteLine($"Exported annotations from '{pdfPath}' to '{xfdfPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to export annotations for '{pdfPath}': {ex.Message}");
            }
        }
    }
}
