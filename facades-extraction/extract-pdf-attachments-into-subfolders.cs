using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputRoot = "Attachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputRoot);

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Get the collection of embedded files (attachments)
        var attachments = pdfDocument.EmbeddedFiles;

        if (attachments == null || attachments.Count == 0)
        {
            Console.WriteLine("No attachments found in the PDF.");
            return;
        }

        // Iterate over each embedded file (FileSpecification)
        foreach (FileSpecification fileSpec in attachments)
        {
            // Original attachment file name
            string attachmentFileName = fileSpec.Name;

            // Create a sub‑directory named after the attachment (without its extension)
            string subDirName = Path.GetFileNameWithoutExtension(attachmentFileName);
            string subDirPath = Path.Combine(outputRoot, subDirName);
            Directory.CreateDirectory(subDirPath);

            // Destination path for the extracted file
            string destinationPath = Path.Combine(subDirPath, attachmentFileName);

            // Save the attachment to disk using the Contents stream
            using (FileStream outStream = File.Create(destinationPath))
            using (Stream content = fileSpec.Contents)
            {
                content?.CopyTo(outStream);
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }
}
