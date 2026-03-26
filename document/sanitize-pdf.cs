using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <input-pdf-path>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        const string outputFileName = "sanitized.pdf";

        try
        {
            using (Document document = new Document(inputPath))
            {
                // Remove metadata and compliance information
                document.RemoveMetadata();
                document.RemovePdfUaCompliance();
                document.RemovePdfaCompliance();

                // Flatten form fields to eliminate hidden form data
                document.Flatten();

                // Save the sanitized PDF
                document.Save(outputFileName);
            }

            Console.WriteLine($"Sanitized PDF saved as '{outputFileName}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
