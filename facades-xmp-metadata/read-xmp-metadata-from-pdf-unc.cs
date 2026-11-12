using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // UNC path to the PDF on a network share
        const string pdfPath = @"\\server\share\documents\sample.pdf";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Use the PdfXmpMetadata facade to read XMP metadata.
            // PdfFileInfo does not expose an XmpMetadata property.
            using (PdfXmpMetadata xmpFacade = new PdfXmpMetadata())
            {
                xmpFacade.BindPdf(pdfPath); // Load the PDF from the UNC location

                // GetXmpMetadata returns a byte[]; convert it to a UTF‑8 string.
                byte[] xmpBytes = xmpFacade.GetXmpMetadata();
                string xmpMetadata = xmpBytes != null && xmpBytes.Length > 0
                    ? Encoding.UTF8.GetString(xmpBytes)
                    : null;

                if (string.IsNullOrEmpty(xmpMetadata))
                {
                    Console.WriteLine("No XMP metadata found in the PDF.");
                }
                else
                {
                    Console.WriteLine("XMP Metadata:");
                    Console.WriteLine(xmpMetadata);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., network access issues, permission problems)
            Console.Error.WriteLine($"Error reading XMP metadata: {ex.Message}");
        }
    }
}
