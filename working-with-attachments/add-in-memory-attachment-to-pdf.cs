using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document entirely in memory
        using (Document doc = new Document())
        {
            // Add a blank page so the PDF is not empty
            doc.Pages.Add();

            // ----- Prepare the attachment data in a MemoryStream -----
            // Example: a simple text file content
            byte[] attachmentBytes = System.Text.Encoding.UTF8.GetBytes("This is the attachment content.");

            // Create a FileSpecification with a display name and description
            FileSpecification fileSpec = new FileSpecification("attachment.txt", "Sample attachment");
            // Assign the in‑memory stream to the Contents property
            fileSpec.Contents = new MemoryStream(attachmentBytes);

            // Add the attachment to the PDF's EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec);

            // ----- Save the PDF to a MemoryStream (no disk I/O) -----
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream); // Saves the PDF (with attachment) into the stream

                // The PDF bytes are now available in pdfStream.
                // For demonstration, output the size of the generated PDF.
                Console.WriteLine($"Generated PDF size (bytes): {pdfStream.Length}");

                // If you later need the PDF as a byte array:
                // byte[] pdfBytes = pdfStream.ToArray();
            }
        }
    }
}