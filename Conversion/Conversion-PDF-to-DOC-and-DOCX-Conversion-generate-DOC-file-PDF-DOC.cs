using System;
using System.IO;
using Aspose.Pdf;               // All SaveOptions subclasses (including DocSaveOptions) are here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDoc = "output.doc";   // change extension to .docx for DOCX output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Configure DOC/DOCX conversion options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Choose the desired output format
                    Format = DocSaveOptions.DocFormat.Doc,          // use .DocX for DOCX output
                    // Set the recognition mode to Flow (text flow based conversion)
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional tweaks – enable bullet detection and adjust proximity
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the PDF as a DOC file using the explicit SaveOptions
                pdfDocument.Save(outputDoc, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{outputDoc}'");
        }
        // GDI+ (gdiplus.dll) is required for some conversion paths on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.Error.WriteLine("Conversion failed: GDI+ is unavailable on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}