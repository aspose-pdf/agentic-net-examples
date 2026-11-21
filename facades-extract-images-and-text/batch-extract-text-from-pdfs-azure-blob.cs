using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Local folders stand in for Azure Blob containers
        const string inputFolder = "pdf-input";
        const string outputFolder = "pdf-output";

        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Extract text from the PDF
            string extractedText;
            using (var extractor = new PdfExtractor())
            {
                // Bind the PDF file (stream or path)
                extractor.BindPdf(pdfPath);
                // Extract text using UTF‑8 encoding
                extractor.ExtractText(Encoding.UTF8);
                // Get the extracted text from a memory stream
                using var ms = new MemoryStream();
                extractor.GetText(ms);
                extractedText = Encoding.UTF8.GetString(ms.ToArray());
            }

            // Write the extracted text to a .txt file with the same base name
            string txtFileName = Path.ChangeExtension(Path.GetFileName(pdfPath), ".txt");
            string txtPath = Path.Combine(outputFolder, txtFileName);
            File.WriteAllText(txtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine("All PDFs processed and text files written.");
    }
}