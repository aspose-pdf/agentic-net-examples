using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory containing PDF files – change as needed or read from args
        string sourceDirectory = @"C:\PdfFiles";
        // Directory where JPEG images will be saved
        string outputDirectory = Path.Combine(sourceDirectory, "JpegImages");

        if (!Directory.Exists(sourceDirectory))
        {
            Console.Error.WriteLine($"Source directory does not exist: {sourceDirectory}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Process each PDF file in the source directory
        foreach (string pdfPath in Directory.GetFiles(sourceDirectory, "*.pdf"))
        {
            string baseFileName = Path.GetFileNameWithoutExtension(pdfPath);

            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Create a JPEG device once (it does not implement IDisposable, so no using)
                JpegDevice jpegDevice = new JpegDevice(new Resolution(300));

                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
                for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
                {
                    // Build output file name: <source>_page<pageNumber>.jpg
                    string jpegPath = Path.Combine(
                        outputDirectory,
                        $"{baseFileName}_page{pageNumber}.jpg");

                    // Convert the specific page to JPEG and save
                    jpegDevice.Process(pdfDocument.Pages[pageNumber], jpegPath);
                }
            }

            Console.WriteLine($"Converted '{pdfPath}' to JPEG images.");
        }

        Console.WriteLine("Batch conversion completed.");
    }
}
