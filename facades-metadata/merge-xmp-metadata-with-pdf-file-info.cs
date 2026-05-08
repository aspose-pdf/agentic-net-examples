using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "merged_metadata.pdf";
        const string tempPdf   = "temp_xmp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load existing XMP metadata, add custom entries, and
        //         save to a temporary PDF file.
        // ------------------------------------------------------------
        PdfXmpMetadata xmp = new PdfXmpMetadata();
        xmp.BindPdf(inputPdf);                     // Initialize facade with the source PDF
        xmp.Add("xmp:CreatorTool", "Aspose.Pdf for .NET"); // Example custom XMP property
        // Additional XMP entries can be added here using xmp.Add(...)
        xmp.Save(tempPdf);                         // Persist XMP changes to a temp file

        // ------------------------------------------------------------
        // Step 2: Load the temporary PDF (which now contains the updated XMP)
        //         and set standard document properties via PdfFileInfo.
        // ------------------------------------------------------------
        PdfFileInfo fileInfo = new PdfFileInfo(tempPdf);
        fileInfo.Title   = "Merged Metadata PDF";
        fileInfo.Author  = "John Doe";
        fileInfo.Subject = "Demo of merging XMP and file info";
        fileInfo.Keywords = "Aspose.Pdf, XMP, metadata";

        // ------------------------------------------------------------
        // Step 3: Merge the PdfFileInfo entries with the existing XMP
        //         metadata and save the final document.
        // ------------------------------------------------------------
        bool success = fileInfo.SaveNewInfoWithXmp(outputPdf);
        if (!success)
        {
            Console.Error.WriteLine("Failed to merge metadata and save the output PDF.");
        }
        else
        {
            Console.WriteLine($"Metadata merged successfully. Output saved to '{outputPdf}'.");
        }

        // Clean up the temporary file
        try { File.Delete(tempPdf); } catch { /* ignore cleanup errors */ }
    }
}