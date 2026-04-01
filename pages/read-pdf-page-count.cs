using System;
using System.IO;
using Aspose.Pdf;

namespace AsposePdfExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Accept a PDF path as a command‑line argument; fall back to "sample.pdf" located next to the exe.
            string pdfPath = args.Length > 0 ? args[0] : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.pdf");

            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
                return;
            }

            // Load the PDF into a memory stream – this avoids keeping the file locked and works without persisting any changes.
            using (FileStream fileStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // reset to beginning for Aspose.Pdf

                // Load the document from the stream.
                using (Document pdfDocument = new Document(memoryStream))
                {
                    int pageCount = pdfDocument.Pages.Count;
                    Console.WriteLine($"Document contains {pageCount} pages.");
                }
            }
        }
    }
}
