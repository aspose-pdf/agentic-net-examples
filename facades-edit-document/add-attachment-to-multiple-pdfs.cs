using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where PDFs with the added attachment will be saved
        const string outputFolder = "OutputPdfs";
        // Path to the file that will be attached to every PDF
        const string attachmentPath = "AttachmentFile.pdf";
        // Name that will appear for the attachment inside the PDF
        const string attachmentName = "AttachedDocument.pdf";

        // Verify that the attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputFolder);

        // Get all PDF files from the input directory
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string sourcePdfPath in pdfFiles)
        {
            string fileName = Path.GetFileName(sourcePdfPath);
            string targetPdfPath = Path.Combine(outputFolder, fileName);

            try
            {
                // Load the source PDF using the Document API (PdfFileEditor does not support AddAttachment)
                Document pdfDoc = new Document(sourcePdfPath);

                // Create a FileSpecification for the attachment
                var fileSpec = new FileSpecification(attachmentName);
                // Load the attachment bytes into a MemoryStream and assign to the specification
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));

                // Add the specification to the EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF to the output folder
                pdfDoc.Save(targetPdfPath);

                Console.WriteLine($"Attachment added to: {targetPdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to process '{sourcePdfPath}': {ex.Message}");
            }
        }
    }
}
