using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputMobi = "output.mobi";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF and ensure deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Set standard metadata
                pdfDoc.Info.Author = "John Doe";

                // Publisher is not a predefined property; add it as custom metadata
                pdfDoc.Info["Publisher"] = "Acme Publishing";

                // Prepare MobiXml save options (default constructor is sufficient)
                MobiXmlSaveOptions mobiOpts = new MobiXmlSaveOptions();

                // Save the document as MobiXml using the options
                pdfDoc.Save(outputMobi, mobiOpts);
            }

            Console.WriteLine($"PDF successfully converted to MobiXml: {outputMobi}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}