using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputTxtPath = "attachment_total_size.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            long totalSize = 0;

            // Enumerate all embedded files (attachments) in the PDF using FileSpecification
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Size is stored in the Params.Size property (bytes)
                totalSize += fileSpec.Params.Size;
            }

            // Write the total size (in bytes) to a text file
            File.WriteAllText(outputTxtPath, totalSize.ToString());
        }

        Console.WriteLine($"Total attachment size written to '{outputTxtPath}'.");
    }
}
