using System;
using System.IO;
using Aspose.Pdf; // Core API namespace

class Program
{
    static void Main()
    {
        // Input PDF containing embedded files (portfolio items)
        const string inputPdfPath = "portfolio.pdf";

        // Index of the embedded file to extract (1‑based as per Aspose.Pdf docs)
        const int embeddedFileIndex = 2; // change as needed

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested index exists
            if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count < embeddedFileIndex)
            {
                Console.Error.WriteLine($"Embedded file index {embeddedFileIndex} is out of range.");
                return;
            }

            // Retrieve the embedded file specification (1‑based index)
            FileSpecification embeddedFileSpec = pdfDoc.EmbeddedFiles[embeddedFileIndex];

            // The original file name (including extension) is stored in the specification
            string originalFileName = embeddedFileSpec.Name;

            // Ensure the embedded file stream (Contents) is present
            if (embeddedFileSpec.Contents == null)
            {
                Console.Error.WriteLine($"Embedded file at index {embeddedFileIndex} has no data.");
                return;
            }

            // Save the embedded file to the current directory using its original name
            using (FileStream outStream = File.Create(originalFileName))
            {
                embeddedFileSpec.Contents.CopyTo(outStream);
            }

            Console.WriteLine($"Extracted embedded file saved as: {originalFileName}");
        }
    }
}
