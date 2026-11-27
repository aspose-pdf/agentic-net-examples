using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentFile = "attachment.txt";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentFile))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFile}");
            return;
        }

        // Load the existing PDF document.
        Document pdf = new Document(inputPdf);

        // Create a file specification for the attachment and add it to the document.
        var fileSpec = new FileSpecification(attachmentFile)
        {
            Description = "Sample attachment",
            // Load the file bytes into a memory stream.
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFile))
        };
        pdf.EmbeddedFiles.Add(fileSpec);

        // Enumerate and display all embedded files (attachments) in the PDF.
        Console.WriteLine("Embedded files in the PDF:");
        foreach (FileSpecification spec in pdf.EmbeddedFiles)
        {
            // Use the Name property – FileName does not exist in Aspose.Pdf.
            Console.WriteLine($"- {spec.Name}");
        }

        // Save the modified PDF with the new attachment.
        pdf.Save(outputPdf);
        Console.WriteLine($"PDF saved with attachment to '{outputPdf}'.");
    }
}
