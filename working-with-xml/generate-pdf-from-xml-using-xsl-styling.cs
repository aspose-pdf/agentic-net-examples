using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Resolve the directory that contains the XML source and the XSL style sheet.
        // Here we assume a sub‑folder named "Data" located next to the executable.
        // Adjust the path as needed for your environment.
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        // Build full paths for the required files.
        string xmlPath = Path.Combine(dataDir, "input.xml");
        string xslPath = Path.Combine(dataDir, "style.xsl");
        string pdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the source files exist before proceeding – this prevents a
        // System.IO.DirectoryNotFoundException at runtime.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML source file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(xslPath))
        {
            Console.WriteLine($"XSL style sheet not found: {xslPath}");
            return;
        }

        // Load options that include the XSL stylesheet for styling.
        XmlLoadOptions loadOptions = new XmlLoadOptions(xslPath);

        // Load the XML and convert it to PDF using the specified style sheet.
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF created with custom colors: {pdfPath}");
    }
}
