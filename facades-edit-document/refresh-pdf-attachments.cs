using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use PdfContentEditor (facade) to manipulate attachments
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPdf);

            // Remove all existing attachments
            editor.DeleteAttachments();

            // Define new files to attach and their descriptions
            string[] attachmentPaths = { "doc1.txt", "image1.png" };
            string[] descriptions = { "Sample text document", "Sample image file" };

            // Add each attachment if the file exists
            for (int i = 0; i < attachmentPaths.Length; i++)
            {
                string path = attachmentPaths[i];
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"Attachment not found: {path}");
                    continue;
                }

                // Add the attachment without any annotation
                editor.AddDocumentAttachment(path, descriptions[i]);
            }

            // Save the updated PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Attachments refreshed. Output saved to '{outputPdf}'.");
    }
}