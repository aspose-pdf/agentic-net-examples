using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string gzipPath = "output.txt.gz";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Extract text from PDF using Aspose.Pdf.Facades
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractText();

                // GetText requires a destination stream – use a MemoryStream and read it back as a string
                string extractedText;
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream); // write extracted text to the stream
                    textStream.Position = 0;        // rewind for reading
                    using (StreamReader reader = new StreamReader(textStream))
                    {
                        extractedText = reader.ReadToEnd();
                    }
                }

                // Write the extracted text to a GZip compressed file
                using (FileStream fileStream = new FileStream(gzipPath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal))
                using (StreamWriter writer = new StreamWriter(gzipStream))
                {
                    writer.Write(extractedText);
                }
            }

            Console.WriteLine($"Extracted text compressed to '{gzipPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
