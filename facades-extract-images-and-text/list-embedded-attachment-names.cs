using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Check for embedded files (attachments) and list their names
        if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
        {
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                Console.WriteLine(fileSpec.Name);
            }
        }
        else
        {
            Console.WriteLine("No embedded files found.");
        }
    }
}
