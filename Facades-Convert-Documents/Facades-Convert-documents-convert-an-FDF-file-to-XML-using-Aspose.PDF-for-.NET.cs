using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input FDF file and output XML file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.fdf> <output.xml>");
            return;
        }

        string fdfPath = args[0];
        string xmlPath = args[1];

        // Verify that the FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Create an empty PDF document (required for importing FDF annotations)
        Document pdfDocument = new Document();

        // Import annotations from the FDF file into the PDF document
        using (FileStream fdfStream = File.OpenRead(fdfPath))
        {
            FdfReader.ReadAnnotations(fdfStream, pdfDocument);
        }

        // Save the resulting document as XML (extension determines format)
        pdfDocument.Save(xmlPath);

        Console.WriteLine($"FDF successfully converted to XML: {xmlPath}");
    }
}