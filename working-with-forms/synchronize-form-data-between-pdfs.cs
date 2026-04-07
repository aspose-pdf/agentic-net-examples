using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source_form.pdf";   // PDF with filled form
        const string targetPdfPath   = "target_form.pdf";   // PDF to receive the data
        const string xfdfTempPath    = "temp_data.xfdf";    // intermediate XFDF file
        const string outputPdfPath   = "target_filled.pdf"; // result

        // Verify files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        try
        {
            // ---------- Export form data (as XFDF) from the source PDF ----------
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Export all annotations (including form fields) to XFDF
                sourceDoc.ExportAnnotationsToXfdf(xfdfTempPath);
            }

            // ---------- Import the exported XFDF into the target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // Read the XFDF file into a stream
                using (FileStream xfdfStream = File.OpenRead(xfdfTempPath))
                {
                    // Import field values from XFDF into the target document
                    XfdfReader.ReadFields(xfdfStream, targetDoc);
                }

                // Save the updated target PDF
                targetDoc.Save(outputPdfPath);
            }

            // Clean up temporary XFDF file
            if (File.Exists(xfdfTempPath))
                File.Delete(xfdfTempPath);

            Console.WriteLine($"Form data synchronized successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}