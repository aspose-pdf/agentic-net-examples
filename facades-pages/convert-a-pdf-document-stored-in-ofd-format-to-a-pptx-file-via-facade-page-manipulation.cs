using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputOfdPath = "input.ofd";
        const string outputPptxPath = "output.pptx";

        if (!File.Exists(inputOfdPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfdPath}");
            return;
        }

        try
        {
            // Load OFD file and convert it to a PDF Document (Document constructor with load options)
            using (Document pdfDoc = new Document(inputOfdPath, new OfdLoadOptions()))
            {
                // Example page manipulation using the PdfPageEditor facade
                // (rotate all pages 0 degrees – replace with any desired manipulation)
                PdfPageEditor editor = new PdfPageEditor();
                editor.BindPdf(pdfDoc);
                editor.Rotation = 0; // no rotation; change as needed
                editor.ApplyChanges();

                // Save the resulting document as PPTX using explicit save options
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                pdfDoc.Save(outputPptxPath, pptxOptions);
            }

            Console.WriteLine($"Conversion completed: {outputPptxPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}