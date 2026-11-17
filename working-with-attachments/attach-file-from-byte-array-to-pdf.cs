using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentName = "data.bin";

        // Load the file to be attached as a byte array.
        if (!File.Exists(attachmentName))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentName}");
            return;
        }
        byte[] fileData = File.ReadAllBytes(attachmentName);

        // Ensure the source PDF exists.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileSpecification from the byte array.
            // The constructor (Stream, name, description) is the correct way to embed a file.
            using (MemoryStream ms = new MemoryStream(fileData))
            {
                FileSpecification fileSpec = new FileSpecification(ms, attachmentName, "Embedded binary data")
                {
                    // Optional: set the modification date of the attachment.
                    Params = { ModDate = DateTime.UtcNow }
                };

                // Attach the file to the PDF.
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"FileSpecification attached and PDF saved to '{outputPdf}'.");
    }
}
