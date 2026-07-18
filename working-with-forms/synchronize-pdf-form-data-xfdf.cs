using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source.pdf";   // PDF with filled form
        const string targetPdfPath   = "target.pdf";   // PDF to receive the data
        const string tempXfdfPath    = "formData.xfdf"; // Intermediate XFDF (XML) file
        const string outputPdfPath   = "target_filled.pdf";

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
            // ---------- Export form data from source PDF to XFDF (XML) ----------
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                // Export all annotations (including form field values) to XFDF
                sourceDoc.ExportAnnotationsToXfdf(tempXfdfPath);
            }

            // ---------- Import the exported XFDF into the target PDF ----------
            using (Document targetDoc = new Document(targetPdfPath))
            using (FileStream xfdfStream = File.OpenRead(tempXfdfPath))
            {
                // Read field values from XFDF and apply them to the target document
                XfdfReader.ReadFields(xfdfStream, targetDoc);

                // Save the updated target PDF
                targetDoc.Save(outputPdfPath);
            }

            // Clean up temporary XFDF file
            if (File.Exists(tempXfdfPath))
                File.Delete(tempXfdfPath);

            Console.WriteLine($"Form data synchronized successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}