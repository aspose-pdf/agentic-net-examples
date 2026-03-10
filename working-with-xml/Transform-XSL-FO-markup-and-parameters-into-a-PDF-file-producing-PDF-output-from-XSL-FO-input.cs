using System;
using System.IO;
using System.Xml.Xsl;               // For XsltArgumentList
using Aspose.Pdf;                  // Core Aspose.Pdf namespace

class XslFoToPdfConverter
{
    static void Main()
    {
        // Paths – adjust as needed
        const string dataDir   = @"YOUR_DATA_DIRECTORY";
        string xslFoPath       = Path.Combine(dataDir, "sample.xslfo");   // Input XSL‑FO file
        string pdfOutputPath   = Path.Combine(dataDir, "output.pdf");    // Desired PDF output

        // Verify input file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Prepare XSL‑FO load options
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Example: set a parameter named "animal" with value "cat"
        // (the XSL‑FO file can reference this parameter via $animal)
        XsltArgumentList args = new XsltArgumentList();
        args.AddParam("animal", "", "cat");
        loadOptions.XsltArgumentList = args;

        // Load the XSL‑FO document and convert it to a PDF Document
        using (Document pdfDocument = new Document(xslFoPath, loadOptions))
        {
            // Save the resulting PDF
            pdfDocument.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF successfully created at: {pdfOutputPath}");
    }
}