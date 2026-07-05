using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the attachment file, and the output PDF
        const string sourcePdfPath = "input.pdf";
        const string attachmentFilePath = "sample.txt";
        const string outputPdfPath = "output_with_attachment.pdf";

        // Unicode description to be stored with the attachment
        const string unicodeDescription = "説明 – тест – اختبار";

        // ------------------------------------------------------------
        // Ensure the source PDF exists – create a minimal PDF if missing
        // ------------------------------------------------------------
        if (!File.Exists(sourcePdfPath))
        {
            using (Document doc = new Document())
            {
                // Add a single blank page
                doc.Pages.Add();
                doc.Save(sourcePdfPath);
            }
        }

        // ------------------------------------------------------------
        // Ensure the attachment file exists – create a simple text file
        // ------------------------------------------------------------
        if (!File.Exists(attachmentFilePath))
        {
            File.WriteAllText(
                attachmentFilePath,
                "Sample attachment content – пример – مثال",
                System.Text.Encoding.UTF8);
        }

        // ---------- Add attachment with Unicode description ----------
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(sourcePdfPath);                                   // Load the source PDF
        editor.AddDocumentAttachment(attachmentFilePath, unicodeDescription); // Add attachment with description
        editor.Save(outputPdfPath);                                      // Save the modified PDF

        // ---------- Verify that the description is correctly stored ----------
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(outputPdfPath);                                // Load the PDF with the attachment
        extractor.ExtractAttachment();                                   // Extract attachment information

        // GetAttachmentInfo returns an array of attachment metadata objects
        var attachmentInfos = extractor.GetAttachmentInfo();
        foreach (var info in attachmentInfos)
        {
            // The Description property holds the Unicode description we set earlier
            Console.WriteLine($"Attachment Name: {info.Name}");
            Console.WriteLine($"Description   : {info.Description}");
        }
    }
}
