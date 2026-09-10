using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "source.pdf";
        const string attachmentPath = "secret.docx";
        const string outputPdfPath  = "encrypted_with_attachment.pdf";

        // Ensure source PDF exists (create a simple one if not)
        if (!File.Exists(inputPdfPath))
        {
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
            {
                // Add a blank page
                Aspose.Pdf.Page page = doc.Pages.Add();

                // Add some visible text
                Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment("Document with encrypted attachment");
                tf.Position = new Aspose.Pdf.Text.Position(100, 700);
                tf.TextState.FontSize = 14;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(tf);

                doc.Save(inputPdfPath);
            }
        }

        // Verify attachment file exists
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Open the PDF
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Get first page (1‑based indexing)
                Aspose.Pdf.Page targetPage = pdfDoc.Pages[1];

                // Define rectangle for the attachment annotation icon
                Aspose.Pdf.Rectangle iconRect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);

                // Create the file specification from a stream (avoids exposing the file path inside the PDF)
                using (FileStream attStream = File.OpenRead(attachmentPath))
                {
                    Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(attStream, Path.GetFileName(attachmentPath));

                    // Create file attachment annotation – the constructor requires the FileSpecification
                    Aspose.Pdf.Annotations.FileAttachmentAnnotation fileAnn =
                        new Aspose.Pdf.Annotations.FileAttachmentAnnotation(targetPage, iconRect, fileSpec);

                    // Optional: set a title that appears in the annotation tooltip
                    fileAnn.Title = "Encrypted attachment";

                    // Add the annotation to the page
                    targetPage.Annotations.Add(fileAnn);
                }

                // Encrypt the entire PDF (including the attachment) so only authorized users can open it
                Aspose.Pdf.Permissions perms = Aspose.Pdf.Permissions.PrintDocument |
                                               Aspose.Pdf.Permissions.ModifyContent |
                                               Aspose.Pdf.Permissions.ExtractContent;

                string userPassword  = "user123";
                string ownerPassword = "owner123";

                pdfDoc.Encrypt(userPassword, ownerPassword, perms, Aspose.Pdf.CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Encrypted PDF with attachment saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
