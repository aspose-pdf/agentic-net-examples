using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document safely using a using block
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfFileInfo facade with the loaded document
            PdfFileInfo pdfInfo = new PdfFileInfo(doc);

            // Set custom metadata field "ReviewedBy"
            pdfInfo.SetMetaInfo("ReviewedBy", "John Doe");

            // Persist the changes to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Custom metadata added and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}