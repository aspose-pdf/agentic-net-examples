using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the directory that contains the source files.
        // Here we use the directory of the executing assembly as the base folder.
        string dataDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        if (string.IsNullOrEmpty(dataDir) || !Directory.Exists(dataDir))
        {
            Console.Error.WriteLine("Data directory could not be resolved.");
            return;
        }

        // XML file that holds the data.
        string xmlFile = Path.Combine(dataDir, "sample.xml");
        // XSL‑FO template that defines the layout.
        string xslFoFile = Path.Combine(dataDir, "template.xslfo");

        // Verify that the required files exist before proceeding.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }
        if (!File.Exists(xslFoFile))
        {
            Console.Error.WriteLine($"XSL‑FO template not found: {xslFoFile}");
            return;
        }

        try
        {
            // Load the XML file and associate the XSL‑FO template.
            XmlLoadOptions xmlLoadOptions = new XmlLoadOptions(xslFoFile);
            using (Document pdfDocument = new Document(xmlFile, xmlLoadOptions))
            {
                // Save the resulting PDF. Output path is a simple file name as required.
                pdfDocument.Save("output.pdf");
                Console.WriteLine("PDF generated successfully: output.pdf");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while generating the PDF: {ex.Message}");
        }
    }
}
