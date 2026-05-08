using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Folder containing source PDF files
        const string inputFolder = "InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = "OutputImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            try
            {
                // Load the PDF document
                Document pdfDocument = new Document(pdfPath);
                int pageCount = pdfDocument.Pages.Count;

                // Define the resolution for the JPEG images (you can adjust as needed)
                var resolution = new Resolution(300);

                // JpegDevice does NOT implement IDisposable, so instantiate it once and reuse.
                JpegDevice jpegDevice = new JpegDevice(resolution);

                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    // Build output file name: <baseName>_page<index>.jpg
                    string outputPath = Path.Combine(outputFolder,
                        $"{baseName}_page{pageIndex}.jpg");

                    // Write the JPEG directly to a file stream.
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageIndex], outStream);
                    }
                }

                Console.WriteLine($"Successfully converted '{pdfPath}' to images.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
