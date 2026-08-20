using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF into a memory stream
            using (FileStream fileStream = File.OpenRead(inputPath))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // reset for reading

                // Initialize PdfFileInfo with the memory stream
                using (PdfFileInfo pdfInfo = new PdfFileInfo(memoryStream))
                {
                    // Modify metadata properties
                    pdfInfo.Title   = "Updated Title";
                    pdfInfo.Author  = "John Doe";
                    pdfInfo.Subject = "Metadata Update Example";
                    pdfInfo.Keywords = "Aspose.Pdf, Facades, Metadata";

                    // Optionally set a custom property
                    pdfInfo.SetMetaInfo("CustomProperty", "CustomValue");

                    // Save the updated PDF to a new file
                    bool success = pdfInfo.SaveNewInfo(outputPath);
                    Console.WriteLine(success
                        ? $"Metadata updated and saved to '{outputPath}'."
                        : $"Failed to save updated PDF to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}