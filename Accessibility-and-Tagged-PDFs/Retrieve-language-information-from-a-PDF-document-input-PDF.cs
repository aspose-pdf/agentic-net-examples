using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input PDF file
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Attempt to read the language from the tagged content root element
            string? language = null;
            if (pdfDoc.TaggedContent != null)
            {
                // The correct property name is 'Language', not 'Lang'
                language = pdfDoc.TaggedContent.RootElement?.Language;
            }

            // Fallback: try to read a Language property from DocumentInfo via reflection
            if (string.IsNullOrEmpty(language) && pdfDoc.Info != null)
            {
                var infoType = pdfDoc.Info.GetType();
                var langProp = infoType.GetProperty("Language");
                if (langProp != null)
                {
                    language = langProp.GetValue(pdfDoc.Info) as string;
                }
            }

            // Output the retrieved language information
            if (!string.IsNullOrEmpty(language))
                Console.WriteLine($"Document language: {language}");
            else
                Console.WriteLine("Language information not found in the PDF.");

            // Optional: save a copy of the document (demonstrates the document‑save rule)
            const string outputPath = "output_copy.pdf";
            pdfDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
