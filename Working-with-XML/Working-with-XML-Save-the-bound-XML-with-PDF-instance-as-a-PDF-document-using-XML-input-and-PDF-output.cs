using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect: args[0] = input XML file path, args[1] = output PDF file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputXmlPath> <outputPdfPath>");
            return;
        }

        string xmlPath = args[0];
        string pdfPath = args[1];

        // Verify that the XML file exists
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file not found at '{xmlPath}'.");
            return;
        }

        try
        {
            // Load the XML file into a PDF document using XmlLoadOptions
            XmlLoadOptions loadOptions = new XmlLoadOptions();

            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save the bound PDF document to the specified output path
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully saved to '{pdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}