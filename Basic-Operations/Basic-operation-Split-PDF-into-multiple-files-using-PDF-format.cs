using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the original document
            Document sourceDoc = new Document(inputPath);

            int partNumber = 1;

            // Move pages one by one to new documents until none remain
            while (sourceDoc.Pages.Count > 0)
            {
                // Create a new empty document
                Document partDoc = new Document();

                // Add the first page from the source to the new document.
                // Aspose.Pdf.Pages.Add copies the page, it does NOT remove it from the source.
                // Therefore we must explicitly delete the page from the source after copying.
                partDoc.Pages.Add(sourceDoc.Pages[1]);
                sourceDoc.Pages.Delete(1); // Remove the transferred page from the source document

                // Build output filename for the current part
                string outputPath = $"output_page_{partNumber}.pdf";

                // Save the split part
                partDoc.Save(outputPath);

                Console.WriteLine($"Saved page {partNumber} to '{outputPath}'");
                partNumber++;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
