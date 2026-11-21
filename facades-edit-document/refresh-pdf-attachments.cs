using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by task specification

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Files that will be added as new attachments
        string[] filesToAdd = new string[]
        {
            "file1.txt",
            "image.png",
            "document.docx"
        };

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        // Verify each file to add exists
        foreach (string f in filesToAdd)
        {
            if (!File.Exists(f))
            {
                Console.Error.WriteLine($"Attachment file not found: {f}");
                return;
            }
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // Remove all existing embedded files (attachments)
            // ------------------------------------------------------------
            if (pdfDoc.EmbeddedFiles != null && pdfDoc.EmbeddedFiles.Count > 0)
            {
                // Collect the names of existing embedded files first
                List<string> existingNames = new List<string>();
                foreach (Aspose.Pdf.FileSpecification spec in pdfDoc.EmbeddedFiles)
                {
                    existingNames.Add(spec.Name);
                }

                // Delete each embedded file by name
                foreach (string name in existingNames)
                {
                    pdfDoc.EmbeddedFiles.Delete(name);
                }
            }

            // ------------------------------------------------------------
            // Add the fresh set of files as embedded files
            // ------------------------------------------------------------
            foreach (string filePath in filesToAdd)
            {
                // Create a FileSpecification with the file name
                Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(Path.GetFileName(filePath));

                // Load the file contents into a memory stream
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));

                // Add the specification to the document's EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF updated successfully. Saved to '{outputPdfPath}'.");
    }
}