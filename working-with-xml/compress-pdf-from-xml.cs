using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Optimization;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML content into a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Enable compression of PDF objects to reduce file size
            OptimizationOptions opt = new OptimizationOptions
            {
                CompressObjects = true
            };
            pdfDoc.OptimizeResources(opt);

            // Save the compressed PDF
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"Compressed PDF saved to '{pdfPath}'.");
    }
}