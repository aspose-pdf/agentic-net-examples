using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string languageCode = "en-US"; // language for hyphenation

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Set the document language – this influences hyphenation during conversion
                ITaggedContent tagged = pdfDoc.TaggedContent;
                tagged.SetLanguage(languageCode);

                // Configure DOCX save options
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    // Output format: DOCX (enum value is DocX, not Docx)
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use full flow recognition for better editability and hyphenation
                    // (Mode property has been removed in recent versions)
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save as DOCX with the specified options
                pdfDoc.Save(outputDocxPath, docxOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
