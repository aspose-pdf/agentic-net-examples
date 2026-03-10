using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath      = "input.pdf";               // Existing PDF
        const string attachmentPath    = "attachment.pdf";          // File to attach
        const string outputPdfPath     = "output_with_attachment.pdf"; // PDF after adding attachment
        const string extractedFilePath = "extracted_attachment.pdf"; // Where the extracted attachment will be saved

        // -----------------------------------------------------------------
        // 1. Add a file attachment to the PDF (no annotation needed)
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // PdfContentEditor is a facade for editing PDF content
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPdfPath);                                   // Load the source PDF
        editor.AddDocumentAttachment(attachmentPath, "Sample attachment"); // Embed the file
        editor.Save(outputPdfPath);                                      // Persist changes
        Console.WriteLine($"Attachment added. Saved to '{outputPdfPath}'.");

        // -----------------------------------------------------------------
        // 2. Extract the previously added attachment from the PDF
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(outputPdfPath); // Load the PDF that contains the attachment
            extractor.ExtractAttachment();    // Prepare extraction of all attachments

            // Retrieve the list of attachment names
            var attachNames = extractor.GetAttachNames(); // IList of attachment file names
            if (attachNames == null || attachNames.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Assume the first attachment is the one we added
            string attachmentName = attachNames[0] as string;
            if (string.IsNullOrEmpty(attachmentName))
            {
                Console.WriteLine("Unable to determine attachment name.");
                return;
            }

            // Extract the attachment to a file with its original name
            extractor.GetAttachment(attachmentName); // Writes the file to the current directory

            // Move/rename the extracted file to the desired output location
            if (File.Exists(attachmentName))
            {
                // Overwrite if the target already exists
                if (File.Exists(extractedFilePath))
                    File.Delete(extractedFilePath);

                File.Move(attachmentName, extractedFilePath);
                Console.WriteLine($"Attachment extracted and saved to '{extractedFilePath}'.");
            }
            else
            {
                Console.WriteLine($"Failed to extract attachment '{attachmentName}'.");
            }
        }
    }
}