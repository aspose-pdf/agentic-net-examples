using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string xmpFilePath   = "metadata.xmp";
        const string outputPdfPath = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmpFilePath))
        {
            Console.Error.WriteLine($"XMP file not found: {xmpFilePath}");
            return;
        }

        try
        {
            // Load the PDF using the Document API (supports XMP metadata)
            Document pdfDocument = new Document(inputPdfPath);

            // Read the external XMP metadata as a string (XMP is XML text)
            string xmpContent = File.ReadAllText(xmpFilePath);

            // Replace the existing XMP block using the Metadata dictionary
            // The key "XMP" is the reserved entry for the full XMP packet.
            if (pdfDocument.Metadata.ContainsKey("XMP"))
                pdfDocument.Metadata["XMP"] = xmpContent;
            else
                pdfDocument.Metadata.Add("XMP", xmpContent);

            // Save the modified PDF
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"XMP metadata replaced and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
