using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all embedded file specifications
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Retrieve the original file name stored in the PDF
                string fileName = fileSpec.Name;

                // Write the attachment's content stream to a file with the original name
                using (FileStream outStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    // Reset the position of the content stream if it supports seeking
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;

                    fileSpec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Saved attachment: {fileName}");
            }
        }
    }
}
