using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF file
        const string outputPath = "output.pdf";  // file with updated metadata

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a memory stream
        using (FileStream fileStream = File.OpenRead(inputPath))
        using (MemoryStream memoryStream = new MemoryStream())
        {
            fileStream.CopyTo(memoryStream);
            memoryStream.Position = 0; // reset position for reading

            // Initialize PdfFileInfo with the memory stream
            using (PdfFileInfo pdfInfo = new PdfFileInfo(memoryStream))
            {
                // Modify metadata properties
                pdfInfo.Title    = "Updated Document Title";
                pdfInfo.Author   = "John Doe";
                pdfInfo.Subject  = "Sample Subject";
                pdfInfo.Keywords = "Aspose, PDF, Metadata";

                // Save the PDF with the new metadata to a new file
                bool success = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(success
                    ? $"Metadata updated and saved to '{outputPath}'."
                    : $"Failed to save updated PDF to '{outputPath}'.");
            }
        }
    }
}