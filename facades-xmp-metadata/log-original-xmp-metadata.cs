using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string auditLogPath = "xmp_audit.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Retrieve original XMP metadata using the PdfXmpMetadata facade
        string originalXmp = null;
        try
        {
            byte[] xmpBytes;
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
            {
                xmpBytes = xmpFacade.GetXmpMetadata(inputPath);
            }
            // Convert the byte[] to a UTF‑8 string (XMP is XML text)
            originalXmp = xmpBytes != null ? Encoding.UTF8.GetString(xmpBytes) : null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read XMP metadata: {ex.Message}");
            originalXmp = null;
        }

        // Log XMP metadata to console
        Console.WriteLine("Original XMP Metadata:");
        Console.WriteLine(originalXmp ?? "(none)");

        // Persist XMP metadata to an audit file
        try
        {
            File.WriteAllText(auditLogPath, originalXmp ?? string.Empty);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write audit log: {ex.Message}");
        }

        // Example modification: add a blank page (optional)
        using (Document doc = new Document(inputPath))
        {
            doc.Pages.Add(); // add a new blank page at the end
            doc.Save(outputPath); // save the modified PDF
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        Console.WriteLine($"XMP audit log saved to '{auditLogPath}'.");
    }
}
