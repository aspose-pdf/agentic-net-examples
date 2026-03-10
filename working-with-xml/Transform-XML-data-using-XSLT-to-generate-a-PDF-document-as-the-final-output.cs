using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the data directory relative to the executable location.
        // Adjust this path as needed for your environment.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Ensure the directory exists – create it if it does not.
        if (!Directory.Exists(dataDir))
        {
            Directory.CreateDirectory(dataDir);
        }

        // Build full paths for the XML and XSLT files.
        string xmlPath = Path.Combine(dataDir, "input.xml");
        string xslPath = Path.Combine(dataDir, "transform.xsl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the required source files exist before attempting the conversion.
        // If they are missing, provide a clear message to the user.
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML source file not found at '{xmlPath}'.");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.Error.WriteLine($"Error: XSLT file not found at '{xslPath}'.");
            return;
        }

        try
        {
            // Load options that include the XSLT file.
            // The XmlLoadOptions constructor attaches the XSLT to the loading process.
            XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

            // Create a PDF document from the XML source using the load options.
            // The Document constructor (string, LoadOptions) performs the XML → PDF conversion.
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save the generated PDF. No additional SaveOptions are required for PDF output.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully created at: {pdfPath}");
        }
        catch (Exception ex)
        {
            // Catch any unexpected exceptions and display a helpful message.
            Console.Error.WriteLine($"An error occurred during PDF generation: {ex.Message}");
        }
    }
}
