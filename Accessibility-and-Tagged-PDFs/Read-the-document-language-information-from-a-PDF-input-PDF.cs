using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect the PDF file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf-path>");
            return;
        }

        string pdfPath = args[0];

        // Verify that the file exists before attempting to load it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // Attempt to read the language information from the document's metadata.
            // DocumentInfo behaves like a dictionary; the "Language" key follows the PDF spec.
            string language = pdfDocument.Info["Language"];

            if (!string.IsNullOrEmpty(language))
            {
                Console.WriteLine($"Document language: {language}");
            }
            else
            {
                Console.WriteLine("Language information not found in the PDF metadata.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}