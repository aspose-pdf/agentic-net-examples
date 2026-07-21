using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "protected.pdf";      // existing password‑protected PDF
        const string userPassword = "user123";           // password that opens the document
        const string attachmentPath = "extra.docx";      // file to attach
        const string outputPdfPath = "protected_with_attachment.pdf";

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

        try
        {
            // Open the encrypted PDF using the user password.
            using (Document pdfDoc = new Document(inputPdfPath, userPassword))
            {
                // Add the attachment as an embedded file.
                pdfDoc.EmbeddedFiles.Add(new FileSpecification(attachmentPath, "Additional document"));

                // Save the PDF (still encrypted) with the new attachment.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
