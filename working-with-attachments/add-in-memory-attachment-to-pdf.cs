using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path (could be any existing PDF)
        const string inputPdfPath = "input.pdf";

        // Output PDF path (final result with attachment)
        const string outputPdfPath = "output_with_attachment.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF into a MemoryStream (no direct disk write for the attachment)
        using (FileStream sourceFile = File.OpenRead(inputPdfPath))
        using (MemoryStream pdfStream = new MemoryStream())
        {
            sourceFile.CopyTo(pdfStream);
            pdfStream.Position = 0; // reset for reading

            // Open the PDF document from the memory stream
            using (Document doc = new Document(pdfStream))
            {
                // ------------------------------------------------------------
                // Create attachment content in memory (e.g., a simple text file)
                // ------------------------------------------------------------
                string attachmentText = "This is the content of the in‑memory attachment.";
                byte[] attachmentBytes = System.Text.Encoding.UTF8.GetBytes(attachmentText);
                using (MemoryStream attachmentStream = new MemoryStream(attachmentBytes))
                {
                    // The FileSpecification constructor expects the stream first, then the file name.
                    FileSpecification fileSpec = new FileSpecification(attachmentStream, "Attachment.txt");

                    // Add the attachment to the PDF's EmbeddedFiles collection.
                    // The key can be any unique identifier; using the file name here.
                    doc.EmbeddedFiles.Add("Attachment.txt", fileSpec);
                }

                // ------------------------------------------------------------
                // Save the modified PDF to a MemoryStream (still no intermediate file)
                // ------------------------------------------------------------
                using (MemoryStream outputStream = new MemoryStream())
                {
                    doc.Save(outputStream);
                    // Write the resulting PDF from memory to the final file on disk
                    File.WriteAllBytes(outputPdfPath, outputStream.ToArray());
                }
            }
        }

        Console.WriteLine($"PDF saved with in‑memory attachment: {outputPdfPath}");
    }
}
