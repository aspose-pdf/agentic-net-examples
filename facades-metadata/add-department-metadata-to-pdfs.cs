using System;
using System.IO;
using Aspose.Pdf.Facades; // Facade classes for PDF metadata manipulation

class Program
{
    static void Main()
    {
        // Folder containing input PDF files
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where updated PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Custom metadata value to assign (could be dynamic per file)
        const string departmentValue = "Finance";

        // Iterate over all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Derive output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            try
            {
                // Initialize the PdfFileInfo facade with the source PDF
                using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
                {
                    // Set custom metadata field "Department"
                    pdfInfo.SetMetaInfo("Department", departmentValue);

                    // Save the updated PDF to the output location
                    // SaveNewInfo writes only the changed metadata without altering other content
                    pdfInfo.SaveNewInfo(outputPath);
                }

                Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}