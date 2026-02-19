using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        try
        {
            // Use the current directory for input and output files.
            string dataDir = Directory.GetCurrentDirectory();

            // Input PDF file.
            string pdfPath = Path.Combine(dataDir, "input.pdf");

            // Output XML file.
            string xmlPath = Path.Combine(dataDir, "output.xml");

            // Ensure the input PDF exists.
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
                return;
            }

            // Load the PDF document and save it as XML.
            using (Document pdfDocument = new Document(pdfPath))
            {
                var saveOptions = new XmlSaveOptions();
                pdfDocument.Save(xmlPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to XML and saved at '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}