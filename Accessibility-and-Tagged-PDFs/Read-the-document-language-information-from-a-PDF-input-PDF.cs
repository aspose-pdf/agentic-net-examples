using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF path (adjust as needed)
        const string inputPdf = "input.pdf";

        // Verify the file exists before attempting to load it
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdf}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdf);

            // Attempt to read the language information.
            // Language is often stored as a custom metadata entry with key "Language".
            // If not present, the result will be null.
            string language = null;
            if (pdfDocument.Info != null && pdfDocument.Info.ContainsKey("Language"))
            {
                language = pdfDocument.Info["Language"];
            }

            // Output the result
            if (!string.IsNullOrEmpty(language))
            {
                Console.WriteLine($"Document language: {language}");
            }
            else
            {
                Console.WriteLine("Language information not found in the PDF metadata.");
            }

            // No modifications are made, but demonstrate the save rule (optional)
            // pdfDocument.Save("output.pdf"); // Uncomment if you need to save a copy
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the PDF: {ex.Message}");
        }
    }
}