using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "example.pdf";
        const string attachmentFilePath = "largeAttachment.bin";
        const string outputPdfPath = "output.pdf";

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

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdfPath);

            using (FileStream fileStream = new FileStream(attachmentFilePath, FileMode.Open, FileAccess.Read))
            {
                // OptimizedMemoryStream with a moderate buffer size to limit memory usage
                OptimizedMemoryStream optStream = new OptimizedMemoryStream(8192);
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    optStream.Write(buffer, 0, bytesRead);
                }
                // Reset position before passing to the editor
                optStream.Position = 0;

                editor.AddDocumentAttachment(optStream, Path.GetFileName(attachmentFilePath), "Large file attachment");
            }

            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and saved to '{outputPdfPath}'.");
    }
}
