using System;
using System.IO;
using Aspose.Pdf;               // Document, MobiXmlSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputMobiPath = "output.mobi";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Explicitly specify the MobiXml save options; otherwise Save() would produce a PDF
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document as a MobiXml file
            pdfDoc.Save(outputMobiPath, mobiOptions);
        }

        Console.WriteLine($"MobiXml file generated at '{outputMobiPath}'.");
    }
}