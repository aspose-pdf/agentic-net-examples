using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToJpegBatch
{
    static void Main()
    {
        // Folder containing source PDFs
        const string inputFolder = @"C:\InputPdfs";
        // Folder where JPEG images will be saved
        const string outputFolder = @"C:\OutputJpegs";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string pdfName = Path.GetFileNameWithoutExtension(pdfPath);

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure the JPEG device (resolution and quality can be adjusted as needed)
                // Use the constructor that accepts both resolution and quality – no ImageQuality property needed
                var jpegDevice = new JpegDevice(new Resolution(150), 90);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    // Build the output file name: <pdfname>_page_<n>.jpg
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"{pdfName}_page_{pageNumber}.jpg");

                    // Convert the current page to a JPEG image and save it directly to disk
                    jpegDevice.Process(pdfDoc.Pages[pageNumber], outputPath);
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
