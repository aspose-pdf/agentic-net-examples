using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PDFs with updated metadata will be saved
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Example static value for the custom field; replace as needed per file
        const string departmentValue = "Finance";

        // Loop through all PDF files in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(inputPath);
            string outputPath = Path.Combine(outputFolder, fileName);

            // Use the PdfFileInfo facade to modify metadata
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Add or update the custom metadata field "Department"
                pdfInfo.SetMetaInfo("Department", departmentValue);

                // Save the PDF with the new metadata to the output location
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Processed: {fileName}");
        }
    }
}