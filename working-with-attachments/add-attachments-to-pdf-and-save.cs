using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdfPath = "input.pdf";

        // Folder where the output PDF will be saved
        const string outputFolder = "Output";

        // Desired output PDF file name
        const string outputPdfName = "output_with_attachments.pdf";

        // Paths of files to attach to the PDF
        string[] attachmentPaths = { "attachment1.txt", "attachment2.jpg" };

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputFolder);

        // Full path for the output PDF
        string outputPdfPath = Path.Combine(outputFolder, outputPdfName);

        // Load the PDF, add attachments (as embedded files), and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Add each file as an embedded file (attachment)
            foreach (string attachPath in attachmentPaths)
            {
                if (!File.Exists(attachPath))
                {
                    Console.Error.WriteLine($"Attachment not found and will be skipped: {attachPath}");
                    continue;
                }

                // Create a FileSpecification with the file name that will appear in the PDF attachment list
                var fileSpec = new FileSpecification(Path.GetFileName(attachPath));
                // Load the file contents into a stream and assign it to the specification
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachPath));
                // Add the specification to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF to the specified output folder
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments to: {outputPdfPath}");
    }
}
