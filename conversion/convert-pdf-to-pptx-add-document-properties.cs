using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Desired output PPTX file path
        const string outputPptxPath = "output.pptx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
        {
            // ----- Add custom document properties -----
            // Subject property (standard PDF metadata)
            pdfDoc.Info.Subject = "Presentation Subject";

            // Company name – PDF does not have a dedicated "Company" field.
            // The Author field is commonly repurposed for this purpose.
            pdfDoc.Info.Author = "Company Name";

            // ----- Convert PDF to PPTX -----
            // Initialize PPTX save options (no additional settings required)
            Aspose.Pdf.PptxSaveOptions pptxOptions = new Aspose.Pdf.PptxSaveOptions();

            // Save the document as a PPTX file using the specified options
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF successfully converted to PPTX and saved as '{outputPptxPath}'.");
    }
}
