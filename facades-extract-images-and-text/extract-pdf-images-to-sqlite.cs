using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfExtractor

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outFolder = "extracted_images"; // folder that stands in for a SQLite BLOB store

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output folder exists – it replaces the database file.
        Directory.CreateDirectory(outFolder);
        string indexPath = Path.Combine(outFolder, "index.csv");
        // Create a simple CSV index that mimics a table of Id, FileName, SizeBytes.
        using (var indexWriter = new StreamWriter(indexPath, false))
        {
            indexWriter.WriteLine("Id,FileName,SizeBytes");
        }

        // Use PdfExtractor to pull images from the PDF.
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage(); // Prepare image extraction

            int imageCounter = 0;
            while (extractor.HasNextImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    // Get the next image into the memory stream (no temp file).
                    extractor.GetNextImage(imageStream);
                    byte[] imageBytes = imageStream.ToArray();

                    string fileName = $"image_{++imageCounter}.png";
                    string filePath = Path.Combine(outFolder, fileName);
                    File.WriteAllBytes(filePath, imageBytes);

                    // Append a row to the CSV index – this is the logical equivalent of inserting a BLOB.
                    using (var indexWriter = new StreamWriter(indexPath, true))
                    {
                        indexWriter.WriteLine($"{imageCounter},{fileName},{imageBytes.Length}");
                    }

                    Console.WriteLine($"Image {imageCounter} extracted to {fileName}");
                }
            }
        }
    }
}
