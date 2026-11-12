using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string logPath    = "xmp_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1. Retrieve the original XMP metadata using the PdfXmpMetadata facade.
        //    PdfFileInfo does not expose XMP data, so we must use the dedicated
        //    facade which provides GetXmpMetadata() / SetXmpMetadata(string).
        // ---------------------------------------------------------------------
        string originalXmp = null;
        using (var xmpFacade = new PdfXmpMetadata())
        {
            xmpFacade.BindPdf(inputPath);
            // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string for logging.
            byte[] xmpBytes = xmpFacade.GetXmpMetadata();
            if (xmpBytes != null && xmpBytes.Length > 0)
            {
                originalXmp = Encoding.UTF8.GetString(xmpBytes);
            }
        }

        // 2. Log the XMP XML for audit purposes.
        if (!string.IsNullOrEmpty(originalXmp))
        {
            File.WriteAllText(logPath, originalXmp);
            Console.WriteLine($"Original XMP metadata logged to '{logPath}'.");
        }
        else
        {
            Console.WriteLine("No XMP metadata found in the source PDF.");
        }

        // ---------------------------------------------------------------------
        // 3. Load the PDF for any further processing. Wrap in using for deterministic disposal.
        // ---------------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Placeholder for modifications (if any)
            // ...

            // Save the (potentially modified) PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
