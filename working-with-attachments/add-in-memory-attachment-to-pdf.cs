using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Tagged;        // For ITaggedContent if needed (not used here)

class Program
{
    static void Main()
    {
        // Path to the source PDF (must exist)
        const string inputPdfPath  = "input.pdf";
        // Path where the resulting PDF with the attachment will be saved
        const string outputPdfPath = "output_with_attachment.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Create a memory stream containing the data to be attached.
        // Here we embed a simple text file; replace the content as needed.
        byte[] attachmentData = System.Text.Encoding.UTF8.GetBytes("This is the content of the in‑memory attachment.");
        using (MemoryStream attachmentStream = new MemoryStream(attachmentData))
        {
            // Ensure the stream position is at the beginning before using it.
            attachmentStream.Position = 0;

            // Open the existing PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdfPath))
            {
                // The FileSpecification constructor expects (Stream, string).
                // The first argument is the stream containing the file data,
                // the second argument is the name that will appear in the PDF attachment list.
                FileSpecification fileSpec = new FileSpecification(attachmentStream, "SampleAttachment.txt");

                // Add the file specification to the document's embedded files collection.
                doc.EmbeddedFiles.Add(fileSpec);

                // Save the modified PDF to the desired output path.
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF saved with in‑memory attachment: {outputPdfPath}");
    }
}
