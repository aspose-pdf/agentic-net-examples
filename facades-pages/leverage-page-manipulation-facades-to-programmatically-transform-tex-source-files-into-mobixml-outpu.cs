using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class TeXToMobiXmlConverter
{
    static void Main()
    {
        // Paths for input TeX file and final MobiXML output.
        const string texInputPath = "input.tex";
        const string mobiXmlOutputPath = "output.mobi";

        // Validate input file existence.
        if (!File.Exists(texInputPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texInputPath}");
            return;
        }

        // Temporary PDF file that will hold the intermediate PDF representation.
        string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        try
        {
            // ------------------------------------------------------------
            // 1. Load TeX source into a PDF Document using TeXLoadOptions.
            // ------------------------------------------------------------
            TeXLoadOptions texLoadOptions = new TeXLoadOptions();
            using (Document pdfDoc = new Document(texInputPath, texLoadOptions))
            {
                // Save the intermediate PDF to a temporary file.
                pdfDoc.Save(tempPdfPath);
            }

            // ------------------------------------------------------------
            // 2. Manipulate pages using PdfFileEditor facade (e.g., add margins).
            // ------------------------------------------------------------
            PdfFileEditor editor = new PdfFileEditor();

            // Example: add 10 units margin on all sides of all pages.
            // The pages array is null to apply to all pages.
            // Use positional arguments because the overload does not support named parameters.
            editor.AddMargins(
                tempPdfPath,               // input PDF
                tempPdfPath,               // output PDF (overwrite)
                null,                      // null = all pages
                10f, 10f, 10f, 10f);       // left, right, top, bottom margins (float literals)

            // ------------------------------------------------------------
            // 3. Load the manipulated PDF and save it as MobiXML.
            // ------------------------------------------------------------
            using (Document finalDoc = new Document(tempPdfPath))
            {
                // Save directly to MobiXML using the appropriate SaveFormat.
                finalDoc.Save(mobiXmlOutputPath, SaveFormat.MobiXml);
            }

            Console.WriteLine($"Conversion completed. MobiXML saved to '{mobiXmlOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary PDF file.
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}
