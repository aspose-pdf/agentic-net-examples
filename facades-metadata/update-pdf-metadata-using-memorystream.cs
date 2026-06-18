using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF into a memory stream
        using (FileStream fileStream = File.OpenRead(inputPdfPath))
        using (MemoryStream memoryStream = new MemoryStream())
        {
            fileStream.CopyTo(memoryStream);
            memoryStream.Position = 0; // reset for reading

            // Initialize PdfFileInfo with the memory stream
            using (PdfFileInfo pdfInfo = new PdfFileInfo(memoryStream))
            {
                // Modify metadata
                pdfInfo.Title  = "Updated Title";
                pdfInfo.Author = "John Doe";
                pdfInfo.Subject = "Sample Subject";
                pdfInfo.Keywords = "Aspose, PDF, Metadata";

                // Save the updated PDF to a new file
                bool success = pdfInfo.SaveNewInfo(outputPdfPath);
                Console.WriteLine(success
                    ? $"Metadata updated and saved to '{outputPdfPath}'."
                    : "Failed to save updated PDF.");
            }
        }
    }
}