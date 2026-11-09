using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string newAttachment = "newAttachment.txt";
        const string outputPdf = "output.pdf";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newAttachment))
        {
            Console.Error.WriteLine($"Attachment file not found: {newAttachment}");
            return;
        }

        // Load the existing PDF
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create a FileSpecification for the new file
            var fileSpec = new FileSpecification(Path.GetFileName(newAttachment), "Additional attachment");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(newAttachment));

            // Add the new file to the EmbeddedFiles collection – existing embedded files are kept automatically
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the result to a new file
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdf}'.");
    }
}
