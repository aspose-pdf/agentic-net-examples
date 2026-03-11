using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file and desired DOCX output
        const string inputCgmPath = "input.cgm";
        const string tempPdfPath  = "temp.pdf";      // intermediate PDF
        const string outputDocxPath = "output.docx";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Convert CGM to PDF using the Facade API (PdfProducer)
            // ------------------------------------------------------------
            // Produce a PDF file from the CGM source.
            PdfProducer.Produce(inputCgmPath, ImportFormat.Cgm, tempPdfPath);

            // ------------------------------------------------------------
            // 2. Load the generated PDF and manipulate its page structure
            //    using another Facade (PdfPageEditor)
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(tempPdfPath))
            {
                // Bind the PDF to the editor. The constructor that accepts a Document
                // automatically binds it, but we can also call BindPdf explicitly.
                using (PdfPageEditor editor = new PdfPageEditor(pdfDoc))
                {
                    // Example manipulation: rotate every page 90 degrees clockwise.
                    // Rotation values must be 0, 90, 180 or 270.
                    editor.Rotation = 90;

                    // Apply the changes to the underlying Document.
                    editor.ApplyChanges();

                    // Save the modified PDF back to the temporary file.
                    // This overwrites the intermediate PDF with the edited version.
                    editor.Save(tempPdfPath);
                }

                // ------------------------------------------------------------
                // 3. Convert the (now edited) PDF to DOCX.
                //    Must use explicit SaveOptions; otherwise the file would be saved as PDF.
                // ------------------------------------------------------------
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Specify DOCX output format.
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Use flow-based text recognition for better layout.
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Enable bullet detection (optional, improves conversion quality).
                    RecognizeBullets = true
                };

                // Save the PDF as a DOCX file using the options above.
                pdfDoc.Save(outputDocxPath, docOptions);
            }

            // Clean up the temporary PDF file.
            if (File.Exists(tempPdfPath))
            {
                File.Delete(tempPdfPath);
            }

            Console.WriteLine($"Conversion completed: '{outputDocxPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}