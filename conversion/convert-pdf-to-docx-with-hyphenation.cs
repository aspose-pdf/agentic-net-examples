using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Set the language for the document – this influences hyphenation during conversion
            ITaggedContent tagged = pdfDocument.TaggedContent;
            tagged.SetLanguage("en-US"); // adjust language code as needed (e.g., "de-DE", "fr-FR")

            // Configure DOCX conversion options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Use Flow mode for full text analysis and better editability
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Enable bullet recognition (optional, improves list handling)
                RecognizeBullets = true,

                // Adjust horizontal proximity if needed (default is 1.0)
                RelativeHorizontalProximity = 2.5f
            };

            // Save the PDF as DOCX using the specified options
            pdfDocument.Save(outputDocxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");
    }
}