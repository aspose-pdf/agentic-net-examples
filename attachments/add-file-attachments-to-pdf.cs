using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Paths to the files that will be attached
        const string attachment1Path = "attachment1.txt";
        const string attachment2Path = "attachment2.jpg";

        // Verify that all required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachment1Path))
        {
            Console.Error.WriteLine($"Attachment not found: {attachment1Path}");
            return;
        }
        if (!File.Exists(attachment2Path))
        {
            Console.Error.WriteLine($"Attachment not found: {attachment2Path}");
            return;
        }

        // Load the PDF, add attachments, and save the result
        using (Document doc = new Document(inputPdf))
        {
            // Create file specifications with distinct descriptions
            var fileSpec1 = new FileSpecification(Path.GetFileName(attachment1Path), "First attachment description");
            fileSpec1.Contents = new MemoryStream(File.ReadAllBytes(attachment1Path));

            var fileSpec2 = new FileSpecification(Path.GetFileName(attachment2Path), "Second attachment description");
            fileSpec2.Contents = new MemoryStream(File.ReadAllBytes(attachment2Path));

            // Add the attachments to the document via the EmbeddedFiles collection
            doc.EmbeddedFiles.Add(fileSpec1);
            doc.EmbeddedFiles.Add(fileSpec2);

            // Save the combined PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with attachments to '{outputPdf}'.");
    }
}
