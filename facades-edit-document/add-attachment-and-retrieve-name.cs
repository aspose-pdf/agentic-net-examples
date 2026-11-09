using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "input.pdf";          // original PDF
        const string attachmentPath = "attachment.txt";    // file to attach
        const string outputPdfPath = "output_with_attachment.pdf";

        // Verify that the required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Work on a copy so the original PDF remains unchanged
        File.Copy(sourcePdfPath, outputPdfPath, true);

        // Load the PDF with Document – PdfFileEditor does not support attachments
        Document pdfDoc = new Document(outputPdfPath);

        // ------------------------------------------------------------
        // Add the attachment using the EmbeddedFiles collection.
        // ------------------------------------------------------------
        // Create a FileSpecification that contains the file name and its data.
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the changes
        pdfDoc.Save(outputPdfPath);

        // Retrieve the list of embedded file names for verification
        string[] attachmentNames = new string[pdfDoc.EmbeddedFiles.Count];
        for (int i = 0; i < pdfDoc.EmbeddedFiles.Count; i++)
        {
            attachmentNames[i] = pdfDoc.EmbeddedFiles[i].Name;
        }

        Console.WriteLine("Attachments present in the PDF:");
        foreach (string name in attachmentNames)
        {
            Console.WriteLine($"- {name}");
        }

        // Verify that the expected attachment name is present
        string expectedName = Path.GetFileName(attachmentPath);
        bool found = Array.Exists(attachmentNames, n =>
            string.Equals(n, expectedName, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(found
            ? $"Attachment '{expectedName}' successfully added."
            : $"Attachment '{expectedName}' not found.");
    }
}
