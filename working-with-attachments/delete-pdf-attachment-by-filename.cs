using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing the attachment
        const string outputPdf = "output.pdf";         // PDF after removal
        const string attachmentName = "example.txt";   // Filename of the embedded file to delete

        // Verify the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, delete the attachment by name, and save the result
        using (Document doc = new Document(inputPdf))
        {
            // The EmbeddedFiles collection provides Delete(string) to remove by filename
            doc.EmbeddedFiles.Delete(attachmentName);

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment \"{attachmentName}\" removed. Saved to \"{outputPdf}\".");
    }
}