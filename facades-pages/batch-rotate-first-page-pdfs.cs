using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where rotated PDFs will be saved
        const string outputFolder = "RotatedPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string fileName = Path.GetFileName(pdfPath);
            string outPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the PDF document
                Document pdfDocument = new Document(pdfPath);

                // Rotate the first page (pages are 1‑based) by 90 degrees clockwise
                // The Rotation enum values are: on0, on90, on180, on270
                pdfDocument.Pages[1].Rotate = Rotation.on90;

                // Save the modified PDF to the output location
                pdfDocument.Save(outPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{fileName}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch rotation of first pages completed.");
    }
}
