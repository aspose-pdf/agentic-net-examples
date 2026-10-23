using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class UpdateZugferdAttachment
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "invoice_original.pdf";
        const string newXmlPath = "invoice_updated.xml";
        const string outputPdfPath = "invoice_updated.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newXmlPath))
        {
            Console.Error.WriteLine($"New ZUGFeRD XML not found: {newXmlPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Remove any existing ZUGFeRD XML attachment
            // ------------------------------------------------------------
            var namesToDelete = new List<string>();
            foreach (FileSpecification fileSpec in pdfDocument.EmbeddedFiles)
            {
                if (!string.IsNullOrEmpty(fileSpec.Name) &&
                    (fileSpec.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) ||
                     fileSpec.Name.IndexOf("ZUGFeRD", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    namesToDelete.Add(fileSpec.Name);
                }
            }
            foreach (string name in namesToDelete)
            {
                pdfDocument.EmbeddedFiles.Delete(name);
            }

            // ------------------------------------------------------------
            // Add the new ZUGFeRD XML attachment
            // ------------------------------------------------------------
            // FileSpecification constructor (string filePath, string description)
            var newSpec = new FileSpecification(newXmlPath, "ZUGFeRD invoice data")
            {
                // MimeType property does not exist in Aspose.Pdf.FileSpecification; it is omitted.
            };
            // Optional: set modification date for completeness
            newSpec.Params.ModDate = DateTime.UtcNow;

            pdfDocument.EmbeddedFiles.Add(newSpec);

            // Save the modified PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with updated ZUGFeRD attachment saved to '{outputPdfPath}'.");
    }
}
