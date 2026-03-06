using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath          = "input.pdf";               // Existing PDF
        const string attachmentFilePath    = "sample.txt";              // File to attach
        const string outputPdfPath         = "output_with_attachment.pdf"; // PDF after attachment
        const string extractedAttachmentPath = "extracted_sample.txt";   // Where to save extracted file

        // -------------------------------------------------
        // 1. Add an attachment to the PDF (no visual annotation)
        // -------------------------------------------------
        try
        {
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Load the source PDF
                editor.BindPdf(inputPdfPath);

                // Attach the external file with a description
                editor.AddDocumentAttachment(attachmentFilePath, "Sample attachment description");

                // Save the modified PDF
                editor.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adding attachment: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // 2. Extract the previously added attachment from the PDF
        // -------------------------------------------------
        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Load the PDF that contains the attachment
                extractor.BindPdf(outputPdfPath);

                // Prepare the extractor to work with attachments
                extractor.ExtractAttachment();

                // The attachment is stored inside the PDF with its original file name.
                // Retrieve it and write it to the desired location.
                // GetAttachment writes the file to the current directory; we rename it afterwards.
                string tempExtractedName = Path.GetFileName(attachmentFilePath);
                extractor.GetAttachment(tempExtractedName);

                // Rename/move to the final desired path
                if (File.Exists(tempExtractedName))
                {
                    File.Move(tempExtractedName, extractedAttachmentPath, overwrite: true);
                    Console.WriteLine($"Attachment extracted to '{extractedAttachmentPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Failed to locate the extracted attachment file.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting attachment: {ex.Message}");
        }
    }
}