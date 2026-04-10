using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileInfo resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF with custom metadata
        const string outputPdf = "output.pdf";  // PDF after adding the new field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // PdfFileInfo works as a facade for PDF meta‑information.
        // It preserves all existing custom metadata; SetMetaInfo only adds/updates the specified key.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Add or update the custom property "Version"
            pdfInfo.SetMetaInfo("Version", "1.0");

            // Save the updated PDF. SaveNewInfo writes only the changed meta‑info,
            // leaving all other metadata untouched.
            bool success = pdfInfo.SaveNewInfo(outputPdf);

            Console.WriteLine(success
                ? $"Metadata updated successfully. Output saved to '{outputPdf}'."
                : "Failed to save the updated PDF.");
        }
    }
}