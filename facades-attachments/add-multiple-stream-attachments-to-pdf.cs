using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF with the high‑level Document API (PdfContentEditor does not support attachments)
        Document pdf = new Document(inputPdf);

        // Add five in‑memory text file attachments
        for (int i = 1; i <= 5; i++)
        {
            string attachmentName = $"Attachment{i}.txt";
            string content = $"This is the content of {attachmentName}";
            byte[] bytes = Encoding.UTF8.GetBytes(content);

            // Create a FileSpecification that represents the attachment
            var fileSpec = new FileSpecification(attachmentName)
            {
                Description = $"Sample attachment {i}",
                // The stream must stay alive until the document is saved, so we create it here
                Contents = new MemoryStream(bytes)
            };

            pdf.EmbeddedFiles.Add(fileSpec);
        }

        // Save the modified PDF
        pdf.Save(outputPdf);
        Console.WriteLine($"PDF saved with attachments: {outputPdf}");

        // Enumerate embedded files and output a simple summary
        Console.WriteLine("Embedded files summary:");
        foreach (FileSpecification file in pdf.EmbeddedFiles)
        {
            // Size is the length of the underlying stream (if available)
            long size = 0;
            if (file.Contents != null && file.Contents.CanSeek)
                size = file.Contents.Length;
            Console.WriteLine($"- Name: {file.Name}, Size: {size} bytes");
        }
    }
}
