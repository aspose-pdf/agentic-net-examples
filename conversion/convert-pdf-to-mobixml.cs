// AsposePdfApi.GlobalUsings.g.cs
// This file satisfies the compiler‑generated global usings reference.
// It can be empty or contain the required global using directives.
// Adding the most common namespaces used in the project.

global using System;
global using System.IO;
global using Aspose.Pdf;

// Program.cs
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the destination MobiXml file.
        const string inputPdfPath = "input.pdf";
        const string outputMobiPath = "output.mobi";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize the save options for MobiXml format with default settings.
            MobiXmlSaveOptions mobiOptions = new MobiXmlSaveOptions();

            // Save the document as MobiXml, explicitly providing the save options.
            pdfDocument.Save(outputMobiPath, mobiOptions);
        }

        Console.WriteLine($"Conversion completed. MobiXml saved to '{outputMobiPath}'.");
    }
}
