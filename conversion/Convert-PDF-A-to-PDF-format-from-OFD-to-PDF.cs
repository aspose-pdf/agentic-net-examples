using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputOfd = "input.ofd";
        const string outputPdf = "output.pdf";
        const string logFile = "conversion.log";

        if (!File.Exists(inputOfd))
        {
            Console.Error.WriteLine($"Input file not found: {inputOfd}");
            return;
        }

        try
        {
            // Load the OFD file (which may contain PDF/A content)
            using (Document doc = new Document())
            {
                doc.LoadFrom(inputOfd, new OfdLoadOptions());

                // Remove PDF/A compliance if present
                doc.RemovePdfaCompliance();

                // Convert to a standard PDF version (e.g., PDF 1.7) and log any conversion issues
                doc.Convert(logFile, PdfFormat.v_1_7, ConvertErrorAction.Delete);

                // Save the result as a regular PDF file
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}