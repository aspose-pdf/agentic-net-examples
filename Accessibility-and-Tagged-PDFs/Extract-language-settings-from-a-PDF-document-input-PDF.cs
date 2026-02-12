using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path – either from command line or a default name
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Aspose.Pdf stores metadata in the DocumentInfo dictionary.
        // The PDF language is usually stored under the "Lang" key (ISO 639‑1/2 code).
        string language = null;
        if (pdfDocument.Info != null && pdfDocument.Info.ContainsKey("Lang"))
        {
            language = pdfDocument.Info["Lang"];
        }
        else if (pdfDocument.Info != null && pdfDocument.Info.ContainsKey("Language"))
        {
            // Fallback – some PDFs may use a different key name
            language = pdfDocument.Info["Language"];
        }

        // Output the result
        if (!string.IsNullOrEmpty(language))
        {
            Console.WriteLine($"Document language: {language}");
        }
        else
        {
            Console.WriteLine("No language setting found in the PDF metadata.");
        }
    }
}