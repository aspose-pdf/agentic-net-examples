using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for any text handling if needed

class Program
{
    static void Main()
    {
        // Input XML file (may contain processing instructions that affect layout)
        const string xmlPath = "input.xml";

        // Optional XSL file that can define page size, styles, etc.
        // If you have an XSL file, set its path; otherwise leave it null.
        const string xslPath = "layout.xsl";

        // Output PDF file
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Prepare load options.
        // If an XSL file is provided, use the constructor that accepts the XSL path.
        XmlLoadOptions loadOptions = string.IsNullOrEmpty(xslPath)
            ? new XmlLoadOptions()
            : new XmlLoadOptions(xslPath);

        // Load the XML (or XML+XSL) into a PDF document.
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Example: enforce a specific page size (e.g., A4) for all pages.
            // This can be used to override sizes that might be defined by processing instructions.
            foreach (Page page in pdfDoc.Pages)
            {
                // SetPageSize(width, height) expects points (1/72 inch). A4 = 595 x 842 points.
                page.SetPageSize(595, 842);
            }

            // Save the resulting PDF.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF generated successfully: {pdfPath}");
    }
}