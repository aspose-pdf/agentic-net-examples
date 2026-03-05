using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XML file that represents a PDF/A document
        const string xmlInputPath  = "input.pdfa.xml";
        // Desired output PDF file (standard PDF)
        const string pdfOutputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(xmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlInputPath}");
            return;
        }

        try
        {
            // Load the XML using XmlLoadOptions (XML is an input‑only format)
            using (Document doc = new Document(xmlInputPath, new XmlLoadOptions()))
            {
                // Remove PDF/A compliance to obtain a regular PDF
                doc.RemovePdfaCompliance();

                // Save the document as a standard PDF
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