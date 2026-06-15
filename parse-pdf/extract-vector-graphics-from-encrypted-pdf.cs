using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // Required for text handling (if needed)

class ExtractVectorGraphics
{
    static void Main()
    {
        // Input encrypted PDF path and password
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string password = "userOrOwnerPassword";

        // Output SVG file that will contain the vector graphics of the PDF
        const string outputSvgPath = "extracted_vectors.svg";

        // Verify the input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF by providing the password to the Document constructor
            // This constructor automatically decrypts the document for further processing.
            using (Document pdfDoc = new Document(encryptedPdfPath, password))
            {
                // Prepare SVG save options.
                // SvgSaveOptions is in the Aspose.Pdf namespace (no separate Saving namespace).
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    // Optional: set the page range if you only need specific pages.
                    // PageIndex = 0;   // first page (zero‑based) – comment out to export all pages
                    // PageCount = 1;   // number of pages to export
                };

                // Save the entire document (or the selected pages) as SVG.
                // The resulting SVG contains the vector graphics from the PDF.
                pdfDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine($"Vector graphics extracted to SVG: '{outputSvgPath}'");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error extracting vector graphics: {ex.Message}");
        }
    }
}