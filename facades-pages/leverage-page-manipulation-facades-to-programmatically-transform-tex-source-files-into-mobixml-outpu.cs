using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class TeXToMobiXmlConverter
{
    static void Main()
    {
        // Paths for input TeX, temporary PDF, and final MobiXML output
        const string texInputPath = "input.tex";
        const string tempPdfPath = "temp.pdf";
        const string mobiXmlPath = "output.mobi.xml";

        // Ensure the input TeX file exists
        if (!File.Exists(texInputPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texInputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load TeX source and convert it to a PDF document
        // -----------------------------------------------------------------
        // TeXLoadOptions provides the necessary options for TeX → PDF conversion
        using (Document texDoc = new Document(texInputPath, new TeXLoadOptions()))
        {
            // Save the intermediate PDF to a temporary file
            texDoc.Save(tempPdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Manipulate PDF pages using a Facade (PdfFileEditor)
        // Example: add uniform margins of 10 units to all pages
        // -----------------------------------------------------------------
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // The AddMargins method does not support named arguments in this version.
        // Use positional arguments: inputFile, outputFile, pageNumbers, left, bottom, right, top.
        pdfEditor.AddMargins(
            tempPdfPath,          // inputFile
            tempPdfPath,          // outputFile (overwrite)
            null,                 // pageNumbers (null = all pages)
            10.0,                 // leftMargin
            10.0,                 // bottomMargin
            10.0,                 // rightMargin
            10.0);                // topMargin

        // -----------------------------------------------------------------
        // Step 3: Load the modified PDF and save it as MobiXML
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(tempPdfPath))
        {
            // MobiXmlSaveOptions resides in the Aspose.Pdf namespace
            MobiXmlSaveOptions mobiOpts = new MobiXmlSaveOptions();

            // Save the document in MobiXML format
            pdfDoc.Save(mobiXmlPath, mobiOpts);
        }

        // Optional cleanup of the temporary PDF file
        try
        {
            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Conversion completed. MobiXML saved to '{mobiXmlPath}'.");
    }
}
