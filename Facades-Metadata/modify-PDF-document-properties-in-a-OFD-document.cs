using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ofd";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the OFD file using the PdfFileInfo facade
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
            {
                // Modify standard document properties
                pdfInfo.Title = "Updated Document Title";
                pdfInfo.Author = "Jane Smith";
                pdfInfo.Subject = "Converted OFD with new metadata";
                pdfInfo.Keywords = "OFD, Aspose.Pdf, metadata";
                pdfInfo.Creator = "MyApp";

                // Add a custom metadata entry
                pdfInfo.SetMetaInfo("CustomProperty", "CustomValue");

                // Save the document with the updated properties.
                // SaveNewInfo writes the modified metadata while preserving the original content.
                pdfInfo.SaveNewInfo(outputPath);
            }

            Console.WriteLine($"Document saved with updated properties to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}