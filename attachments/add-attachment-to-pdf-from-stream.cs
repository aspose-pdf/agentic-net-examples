using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentName = "example.txt";
        const string attachmentDescription = "Sample attachment added from a stream.";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Prepare the attachment data as a memory stream.
            byte[] attachmentData = Encoding.UTF8.GetBytes("This is the content of the attachment.");
            using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
            {
                // Create a FileSpecification from the stream, providing the file name.
                FileSpecification fileSpec = new FileSpecification(attachmentStream, attachmentName);
                // Set the description that will appear in the PDF attachment list.
                fileSpec.Description = attachmentDescription;

                // Add the file specification to the document's EmbeddedFiles collection.
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment to '{outputPdfPath}'.");
    }
}
