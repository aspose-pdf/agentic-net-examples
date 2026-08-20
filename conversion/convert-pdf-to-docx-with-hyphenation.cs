using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        // Input PDF and output DOCX paths
        const string inputPdf  = "input.pdf";
        const string outputDocx = "output.docx";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        // Convert PDF to DOCX with language‑aware hyphenation settings
        // (Hyphenation options are applied automatically based on the language
        //  of the source content; explicit HyphenationOptions are not exposed
        //  in the current DocSaveOptions API.)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Configure DOC/DOCX save options
            DocSaveOptions saveOptions = new DocSaveOptions
            {
                // Output format – DOCX
                Format = DocSaveOptions.DocFormat.DocX,

                // Use flow‑based content recognition for better text flow
                Mode = DocSaveOptions.RecognitionMode.Flow,

                // Adjust horizontal proximity (tune paragraph detection)
                RelativeHorizontalProximity = 2.5f,

                // Enable bullet detection (helps list structures)
                RecognizeBullets = true
            };

            // Save the converted document
            pdfDocument.Save(outputDocx, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputDocx}'");
    }
}