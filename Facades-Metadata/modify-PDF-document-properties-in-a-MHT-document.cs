using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source MHT file and the resulting PDF file
        const string mhtPath   = "input.mht";
        const string pdfPath   = "output.pdf";

        // Verify source file exists
        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"Source file not found: {mhtPath}");
            return;
        }

        // Load the MHT document into a PDF Document using MhtLoadOptions
        using (Document pdfDoc = new Document(mhtPath, new MhtLoadOptions()))
        {
            // Create a PdfFileInfo facade bound to the loaded document
            PdfFileInfo info = new PdfFileInfo(pdfDoc);

            // Modify standard document properties
            info.Title  = "Converted MHT Document";
            info.Author = "John Doe";
            info.Subject = "Sample conversion from MHT to PDF";
            info.Keywords = "MHT, PDF, Aspose.Pdf.Facades";

            // Add a custom metadata entry
            info.SetMetaInfo("CustomProperty", "CustomValue");

            // Save the updated PDF document with the new properties
            info.SaveNewInfo(pdfPath);
        }

        Console.WriteLine($"MHT converted and properties updated: {pdfPath}");
    }
}