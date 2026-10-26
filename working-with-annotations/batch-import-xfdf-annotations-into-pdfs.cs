using System;
using System.IO;
using Aspose.Pdf;

class BatchXfdfImporter
{
    static void Main()
    {
        // Folder containing PDFs and their corresponding XFDF files
        const string inputFolder = @"C:\Docs\Input";
        // Folder where the PDFs with merged annotations will be saved
        const string outputFolder = @"C:\Docs\Output";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Enumerate all PDF files in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = Path.Combine(inputFolder, baseName + ".xfdf");

            // If there is no matching XFDF file, just copy the original PDF
            if (!File.Exists(xfdfPath))
            {
                string destCopy = Path.Combine(outputFolder, Path.GetFileName(pdfPath));
                File.Copy(pdfPath, destCopy, overwrite: true);
                Console.WriteLine($"No XFDF for '{baseName}'. Copied original PDF.");
                continue;
            }

            try
            {
                // Load the PDF document
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Import XFDF annotations and merge them into the PDF
                    pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                    // Save the annotated PDF to the output folder
                    string outputPdfPath = Path.Combine(outputFolder, baseName + "_annotated.pdf");
                    pdfDoc.Save(outputPdfPath);
                    Console.WriteLine($"Merged annotations for '{baseName}' -> '{outputPdfPath}'.");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{baseName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch XFDF import completed.");
    }
}
