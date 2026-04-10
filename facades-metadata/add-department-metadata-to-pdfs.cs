using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF files to process
        const string inputFolder  = @"C:\InputPdfs";
        // Folder where the updated PDFs will be saved
        const string outputFolder = @"C:\OutputPdfs";

        // Verify that the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: '{inputFolder}'. No files were processed.");
            return; // Exit gracefully
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Loop through all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine the output file path (same name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Use PdfFileInfo facade to modify metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Set the custom metadata field "Department"
                pdfInfo.SetMetaInfo("Department", "Finance");

                // Save the updated PDF to a new file
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }
    }
}
