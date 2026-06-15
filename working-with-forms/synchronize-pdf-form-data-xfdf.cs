using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // XfdfReader resides here

class Program
{
    static void Main()
    {
        const string sourcePdfPath   = "source_form.pdf";   // PDF with filled form fields
        const string targetPdfPath   = "target_form.pdf";   // PDF to receive the data
        const string outputPdfPath   = "target_form_updated.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPdfPath}");
            return;
        }

        try
        {
            // 1. Export form data (as XFDF) from the source PDF
            using (var sourceDoc = new Document(sourcePdfPath))
            using (var xfdfStream = new MemoryStream())
            {
                // ExportAnnotationsToXfdf also includes form field values
                sourceDoc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // reset for reading

                // 2. Import the XFDF data into the target PDF
                using (var targetDoc = new Document(targetPdfPath))
                {
                    // XfdfReader reads the XFDF stream and populates the fields in targetDoc
                    XfdfReader.ReadFields(xfdfStream, targetDoc);

                    // 3. Save the updated target PDF
                    targetDoc.Save(outputPdfPath);
                }
            }

            Console.WriteLine($"Form data synchronized successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during synchronization: {ex.Message}");
        }
    }
}