using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "attachmentCount.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // EmbeddedFiles collection holds all file attachments (FileSpecification objects)
            int attachmentCount = doc.EmbeddedFiles.Count;

            // Optionally enumerate each embedded file to log its name
            for (int i = 1; i <= doc.EmbeddedFiles.Count; i++) // 1‑based indexing
            {
                FileSpecification fileSpec = doc.EmbeddedFiles[i];
                Console.WriteLine($"Attachment {i}: {fileSpec.Name}");
            }

            // Write the count to a text file
            File.WriteAllText(outputPath, attachmentCount.ToString());
            Console.WriteLine($"Attachment count ({attachmentCount}) written to '{outputPath}'.");
        }
    }
}
