using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <pdfPath> <fdfPath> <outputXmlPath>
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <pdfPath> <fdfPath> <outputXmlPath>");
            return;
        }

        string pdfPath = args[0];
        string fdfPath = args[1];
        string outputPath = args[2];

        // Verify input files exist
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
            // Load the existing PDF document
            Document pdfDoc = new Document(pdfPath);

            // Read FDF annotations and import them into the PDF
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the updated document as XML (extension determines format)
            pdfDoc.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}