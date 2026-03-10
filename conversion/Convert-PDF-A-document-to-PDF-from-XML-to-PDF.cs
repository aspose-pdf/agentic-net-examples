using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file that represents a PDF/A document
        const string xmlInputPath  = "input.pdfa.xml";
        // Output PDF file (standard PDF, PDF/A compliance removed)
        const string pdfOutputPath = "output.pdf";

        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML using XmlLoadOptions (PDF/A content is interpreted)
            using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Remove PDF/A compliance flags – the resulting file will be a regular PDF
                doc.RemovePdfaCompliance();

                // Save as a standard PDF. No SaveOptions needed for PDF output.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Conversion completed. PDF saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}