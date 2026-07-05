using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "input_pdfs";
        // Folder where updated PDFs will be written
        const string outputFolder = "output_pdfs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists before trying to enumerate files
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. No PDFs were processed.");
            return;
        }

        // Loop through all PDF files in the input folder
        foreach (string sourcePath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Preserve original file name for the output
            string fileName   = Path.GetFileName(sourcePath);
            string targetPath = Path.Combine(outputFolder, fileName);

            // Initialize the Facade for accessing PDF meta‑information
            PdfFileInfo pdfInfo = new PdfFileInfo(sourcePath);

            // Add or update a custom metadata field named "Department"
            // (value can be changed per file as needed)
            pdfInfo.SetMetaInfo("Department", "Finance");

            // Persist the changes to a new file
            pdfInfo.SaveNewInfo(targetPath);
        }

        Console.WriteLine("Custom metadata 'Department' added to all PDFs.");
    }
}
