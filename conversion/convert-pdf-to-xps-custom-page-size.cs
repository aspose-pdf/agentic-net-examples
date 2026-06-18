using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, XpsSaveOptions)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXps = "output.xps";

        // Define custom page size (width x height in points).
        // Example: A4 landscape size – 842pt width, 595pt height.
        double customWidth  = 842;   // points
        double customHeight = 595;   // points

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Apply the custom size to every page.
            foreach (Page page in doc.Pages)
            {
                page.SetPageSize(customWidth, customHeight);
            }

            // Initialize XPS save options (no special settings required here).
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS using the explicit SaveOptions.
            doc.Save(outputXps, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXps}");
    }
}