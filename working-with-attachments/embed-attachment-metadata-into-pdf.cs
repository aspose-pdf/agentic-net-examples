using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine("Input PDF or attachment file not found.");
            return;
        }

        // Load the source PDF (create‑load‑save lifecycle)
        using (Document doc = new Document(inputPdfPath))
        {
            // Add an embedded file (attachment) to the PDF
            using (FileStream attachmentStream = File.OpenRead(attachmentFilePath))
            {
                string attachmentName = Path.GetFileName(attachmentFilePath);
                // Correct constructor order: (Stream, string)
                FileSpecification fileSpec = new FileSpecification(attachmentStream, attachmentName);
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Store attachment metadata in the document information dictionary
            doc.Info.Add("AttachmentName", Path.GetFileName(attachmentFilePath));
            doc.Info.Add("AttachmentDescription", "Sample attachment added via Aspose.Pdf");

            // Configure conversion options to transfer Info entries to Metadata (PDF 1.6 used for compatibility)
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.v_1_6);
            convOptions.IsTransferInfo = true; // embed Info dictionary into Metadata

            // Apply the conversion options (conversion to the same format)
            doc.Convert(convOptions);

            // Save the modified PDF (create‑save lifecycle)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment metadata to '{outputPdfPath}'.");
    }
}
