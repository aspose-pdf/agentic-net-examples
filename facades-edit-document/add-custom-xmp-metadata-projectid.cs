using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Add custom XMP metadata field "ProjectID" with value "12345"
        // ------------------------------------------------------------
        using (PdfXmpMetadata xmp = new PdfXmpMetadata())
        {
            // Bind the existing PDF
            xmp.BindPdf(inputPath);

            // Add a custom XMP entry. The key can be any string; here we use "ProjectID"
            xmp.Add("ProjectID", "12345");

            // Save the PDF with the new XMP metadata to a temporary file
            // (we will later add the same info to the document properties)
            string tempPath = Path.Combine(Path.GetDirectoryName(outputPath) ?? "", "temp_with_xmp.pdf");
            xmp.Save(tempPath);

            // ------------------------------------------------------------
            // 2. Ensure the same information appears in the document properties
            // ------------------------------------------------------------
            using (PdfFileInfo fileInfo = new PdfFileInfo())
            {
                // Bind the PDF that already contains the XMP metadata
                fileInfo.BindPdf(tempPath);

                // Set a custom property in the document's Info dictionary
                fileInfo.SetMetaInfo("ProjectID", "12345");

                // Save the final PDF, preserving the XMP metadata added earlier
                // SaveNewInfoWithXmp keeps existing XMP data while updating the Info dictionary
                fileInfo.SaveNewInfoWithXmp(outputPath);
            }

            // Clean up the temporary file
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }

        Console.WriteLine($"PDF saved with custom XMP field and document property: {outputPath}");
    }
}