using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "secured.pdf";
        const string userPassword      = "userPass";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath      = "secured_with_attachment.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the user password
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath, userPassword))
            {
                // Create a file specification for the attachment
                var fileSpec = new Aspose.Pdf.FileSpecification(Path.GetFileName(attachmentFilePath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath));

                // Add the file specification to the EmbeddedFiles collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // Save the PDF (encryption is preserved)
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