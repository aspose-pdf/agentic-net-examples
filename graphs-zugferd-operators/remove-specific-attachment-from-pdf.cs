using System;
using System.IO;
using Aspose.Pdf; // Provides Document and EmbeddedFileCollection

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF containing attachments
        const string outputPdf = "output.pdf";         // PDF after removal
        const string attachmentToRemove = "example.txt"; // Exact filename of the attachment to delete

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Delete the specified attachment by its filename.
            // The EmbeddedFiles collection exposes Delete(string name).
            doc.EmbeddedFiles.Delete(attachmentToRemove);

            // Save the modified PDF. No SaveOptions needed because we keep PDF format.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Attachment \"{attachmentToRemove}\" removed. Saved to '{outputPdf}'.");
    }
}