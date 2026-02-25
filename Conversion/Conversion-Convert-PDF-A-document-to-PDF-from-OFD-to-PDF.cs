using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace (required by the task)

class Program
{
    static void Main()
    {
        const string inputOfd  = "input.ofd";   // OFD source file (PDF/A compliant)
        const string outputPdf = "output.pdf";  // Desired PDF output file

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"Source file not found: {inputOfd}");
            return;
        }

        try
        {
            // Load the OFD file using the specific OFD load options.
            // OfdLoadOptions derives from LoadOptions and is concrete.
            var loadOptions = new OfdLoadOptions();

            // Create the Document instance with the OFD file and load options.
            using (Document doc = new Document(inputOfd, loadOptions))
            {
                // Save the document as a regular PDF.
                // No special SaveOptions are required for PDF output.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed: '{outputPdf}'");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}