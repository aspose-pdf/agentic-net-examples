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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind PdfFileInfo to the loaded document
            using (PdfFileInfo pdfInfo = new PdfFileInfo(doc))
            {
                // Generate a new GUID
                string newGuid = Guid.NewGuid().ToString();

                // Store the GUID as a custom metadata entry named "DocumentId"
                pdfInfo.SetMetaInfo("DocumentId", newGuid);

                // Save the updated PDF with the new Document ID
                bool saved = pdfInfo.SaveNewInfo(outputPath);
                Console.WriteLine(saved
                    ? $"Document ID set to {newGuid} and saved to '{outputPath}'."
                    : "Failed to save the updated PDF.");
            }
        }
    }
}