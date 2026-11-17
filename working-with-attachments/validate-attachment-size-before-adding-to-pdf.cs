using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const long maxAttachmentSize = 5 * 1024 * 1024; // 5 MB limit

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Files you want to attach to the PDF
        string[] filesToAttach = { "image1.png", "document.docx", "largefile.zip" };

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPdf))
        {
            foreach (string filePath in filesToAttach)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Skipping missing file: {filePath}");
                    continue;
                }

                FileInfo info = new FileInfo(filePath);
                if (info.Length > maxAttachmentSize)
                {
                    Console.WriteLine($"Skipping {filePath}: size {info.Length} exceeds limit of {maxAttachmentSize} bytes.");
                    continue;
                }

                // Create a FileSpecification with the file's contents and add it to EmbeddedFiles
                var fileSpec = new FileSpecification(Path.GetFileName(filePath), Path.GetFileName(filePath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));
                doc.EmbeddedFiles.Add(fileSpec);
                Console.WriteLine($"Added attachment: {filePath}");
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
