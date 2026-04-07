using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "attachment.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Delete the embedded file by its filename
                // Delete(string) removes the attachment if it exists; otherwise it does nothing
                doc.EmbeddedFiles.Delete(attachmentName);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment '{attachmentName}' removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}