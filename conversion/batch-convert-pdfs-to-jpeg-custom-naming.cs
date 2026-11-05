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
        const string outputFolder = "OutputJpegs";
        // Custom naming pattern: {pdfName}_page{pageNumber}.jpg
        const string namingPattern = "{pdfName}_page{pageNumber}.jpg";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        foreach (string pdfPath in pdfFiles)
        {
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);

            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    // Build the output file name according to the custom pattern
                    string outputFileName = namingPattern
                        .Replace("{pdfName}", pdfFileName)
                        .Replace("{pageNumber}", pageIndex.ToString());
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Create a JPEG device (does NOT implement IDisposable)
                    var jpegDevice = new JpegDevice(new Resolution(300), 90);

                    // Render the page to a JPEG file via a FileStream
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        jpegDevice.Process(doc.Pages[pageIndex], outStream);
                    }

                    Console.WriteLine($"Saved: {outputPath}");
                }
            }
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
