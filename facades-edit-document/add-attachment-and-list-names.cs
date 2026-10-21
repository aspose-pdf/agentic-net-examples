using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the file to embed, and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Verify that the attachment file exists
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Create a working copy of the PDF
        File.Copy(sourcePdfPath, outputPdfPath, true);

        // Load the PDF document
        Document pdfDoc = new Document(outputPdfPath);

        // ---------------------------------------------------------------------
        // Add the file as an embedded file (the correct way in Aspose.PDF).
        // ---------------------------------------------------------------------
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath));
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the changes back to the output file.
        pdfDoc.Save(outputPdfPath);

        // Retrieve all embedded file names from the updated PDF
        Console.WriteLine("Embedded files present in the PDF:");
        foreach (FileSpecification fs in pdfDoc.EmbeddedFiles)
        {
            Console.WriteLine($"- {fs.Name}");
        }
    }
}
