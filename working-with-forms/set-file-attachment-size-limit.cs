using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "fileAttachmentField"; // name of the file attachment field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Retrieve the file attachment field from the document's form collection.
        // Use dynamic to avoid compile‑time dependency on a specific field class.
        dynamic attachmentField = doc.Form[fieldName];
        if (attachmentField == null)
        {
            Console.Error.WriteLine($"Field '{fieldName}' not found.");
            return;
        }

        // Set the maximum allowed file size to 2 MB (bytes).
        // The MaxFileSize property exists on the actual FileAttachmentField type at runtime.
        attachmentField.MaxFileSize = 2 * 1024 * 1024; // 2097152 bytes

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with 2 MB file size constraint on field '{fieldName}' to '{outputPath}'.");
    }
}
