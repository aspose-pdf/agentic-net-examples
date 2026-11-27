using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string attachmentName = "sample.txt";
        const string attachmentDescription = "Sample text attachment";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF using the high‑level Document API.
        Document pdf = new Document(inputPdf);

        // Prepare the attachment data.
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the attachment.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            // Create a FileSpecification that represents the embedded file.
            var fileSpec = new FileSpecification(attachmentName)
            {
                Description = attachmentDescription,
                // The Contents property expects a stream containing the file bytes.
                Contents = attachmentStream
            };

            // Add the file specification to the PDF's EmbeddedFiles collection.
            pdf.EmbeddedFiles.Add(fileSpec);
        }

        // Save the modified PDF.
        pdf.Save(outputPdf);

        Console.WriteLine($"PDF saved with attachment to '{outputPdf}'.");
    }
}
