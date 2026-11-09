using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";          // source PDF
        const string attachmentFilePath = "attachment.txt"; // file to attach
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Copy the source PDF to a new file that will receive the attachment
        File.Copy(inputPdfPath, outputPdfPath, overwrite: true);

        // Load the PDF with the Document API (PdfFileEditor does not support AttachFile)
        Document pdfDoc = new Document(outputPdfPath);

        // Create a FileSpecification for the attachment
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath), "MyAttachment");
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

        // Add the file to the PDF's EmbeddedFiles collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the changes back to the output file
        pdfDoc.Save(outputPdfPath);

        // Retrieve the list of embedded file names to confirm the addition
        Console.WriteLine("Embedded files in the PDF:");
        foreach (FileSpecification spec in pdfDoc.EmbeddedFiles)
        {
            Console.WriteLine($"- {spec.Name}");
        }
    }
}
