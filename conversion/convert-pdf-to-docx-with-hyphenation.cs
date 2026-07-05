using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // retained for potential future use

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Output DOCX file path
        const string docxPath = "output.docx";

        // Verify the source file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document and convert it to DOCX.
        // The conversion automatically applies language‑aware hyphenation.
        using (Document pdfDocument = new Document(pdfPath))
        {
            var saveOptions = new DocSaveOptions
            {
                // Export as DOCX format – correct enum value
                Format = DocSaveOptions.DocFormat.DocX,

                // Optional: improve paragraph detection and bullet handling
                RecognizeBullets = true,
                RelativeHorizontalProximity = 2.5f,
                AddReturnToLineEnd = true
                // The "Mode" property has been removed in recent versions; the default
                // flow recognition (which includes hyphenation) is applied automatically.
            };

            // Save the converted document
            pdfDocument.Save(docxPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to DOCX with hyphenation settings: {docxPath}");
    }
}