using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_projectid.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind XMP metadata facade to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Add a custom XMP field "ProjectID" with value "12345"
            xmp.Add("ProjectID", "12345");

            // Save the updated metadata back to the PDF.
            // PdfFileInfo facade writes the XMP changes while preserving other properties.
            PdfFileInfo fileInfo = new PdfFileInfo(doc);
            bool saved = fileInfo.SaveNewInfoWithXmp(outputPath);

            if (!saved)
            {
                Console.Error.WriteLine("Failed to save PDF with updated XMP metadata.");
            }
            else
            {
                Console.WriteLine($"PDF saved with ProjectID metadata to '{outputPath}'.");
            }
        }
    }
}