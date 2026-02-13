using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPdf> <inputFdf> <outputXml>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <pdfPath> <fdfPath> <outputXmlPath>");
            return;
        }

        string pdfPath = args[0];
        string fdfPath = args[1];
        string outputPath = args[2];

        // Verify that the source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDoc = new Document(pdfPath);

            // Import annotations from the FDF file into the PDF
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting document as XML (extension determines format)
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Document saved as XML to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}