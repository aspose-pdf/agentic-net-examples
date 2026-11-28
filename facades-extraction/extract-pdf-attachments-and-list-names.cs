using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string attachmentsFolder = "Attachments";
        const string namesFilePath = "AttachmentNames.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(attachmentsFolder);

        try
        {
            // Extract attachments using PdfExtractor (it implements IDisposable)
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractAttachment(attachmentsFolder);
            }

            // Retrieve the original attachment names via the Document API (the newer way)
            using (Document doc = new Document(pdfPath))
            {
                using (StreamWriter writer = new StreamWriter(namesFilePath, false))
                {
                    // Document.EmbeddedFiles contains FileSpecification objects
                    foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                    {
                        // The Name property holds the original filename stored in the PDF
                        writer.WriteLine(fileSpec.Name);
                    }
                }
            }

            Console.WriteLine($"Attachments extracted to '{attachmentsFolder}'.");
            Console.WriteLine($"Attachment names written to '{namesFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
