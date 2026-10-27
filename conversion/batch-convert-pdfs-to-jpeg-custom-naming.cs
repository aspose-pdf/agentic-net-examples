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
        // Folder where JPEG images will be written
        const string outputFolder = "OutputImages";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Retrieve all PDF files in the input directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);

        foreach (string pdfPath in pdfFiles)
        {
            // Load each PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Build a custom file name: <pdfname>_page<pageNum>.jpg
                    string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                    string jpegPath = Path.Combine(outputFolder, $"{baseName}_page{pageNum}.jpg");

                    // JpegDevice is instantiated directly (it does not implement IDisposable)
                    // Resolution (DPI) and quality are supplied via the constructor
                    var jpegDevice = new JpegDevice(new Resolution(150), 90);

                    // Write the JPEG image to a file stream (the stream is disposable)
                    using (FileStream outStream = new FileStream(jpegPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(pdfDoc.Pages[pageNum], outStream);
                    }

                    Console.WriteLine($"Saved: {jpegPath}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
