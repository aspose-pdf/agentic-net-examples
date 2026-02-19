using System;
using System.IO;
using Aspose.Pdf;

class ExtractPdfLanguage
{
    static void Main(string[] args)
    {
        // Expect the PDF file path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: ExtractPdfLanguage <input-pdf-path>");
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

            // Attempt to retrieve the language setting from XMP metadata (dc:language)
            string language = null;
            if (pdfDocument.Metadata != null && pdfDocument.Metadata.ContainsKey("dc:language"))
            {
                // Metadata values are of type XmpValue; convert to string explicitly
                language = pdfDocument.Metadata["dc:language"]?.ToString();
            }

            // Fallback: check for a custom metadata key named "Language"
            if (string.IsNullOrEmpty(language) && pdfDocument.Metadata != null && pdfDocument.Metadata.ContainsKey("Language"))
            {
                language = pdfDocument.Metadata["Language"]?.ToString();
            }

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
