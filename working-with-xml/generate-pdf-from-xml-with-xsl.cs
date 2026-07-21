using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for any text‑related operations (not used here but kept for completeness)

class Program
{
    static void Main()
    {
        // Input XML file that may contain a processing instruction referencing an external CSS file.
        const string xmlFile   = "input.xml";

        // Optional XSL stylesheet that can be used to transform the XML into PDF layout.
        // If your XML relies on CSS for styling, you can first convert the CSS to an XSL
        // (or use an existing XSL that incorporates the CSS rules) and pass its path here.
        // If no XSL is required, set xslFile to null or an empty string.
        const string xslFile   = "style.xsl";

        // Output PDF file.
        const string pdfFile   = "output.pdf";

        // Validate input files.
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML file not found: {xmlFile}");
            return;
        }

        if (!string.IsNullOrEmpty(xslFile) && !File.Exists(xslFile))
        {
            Console.Error.WriteLine($"XSL file not found: {xslFile}");
            return;
        }

        // -----------------------------------------------------------------
        // Load the XML document.
        // XmlLoadOptions can be constructed with an XSL file/stream.
        // When an XSL is supplied, Aspose.Pdf applies the transformation
        // during loading, allowing you to map CSS‑derived styling into the PDF.
        // -----------------------------------------------------------------
        XmlLoadOptions loadOptions = string.IsNullOrEmpty(xslFile)
            ? new XmlLoadOptions()                 // No XSL – load XML as‑is.
            : new XmlLoadOptions(xslFile);         // Load XML and apply XSL.

        // Use a using block for deterministic disposal (rule: document-disposal-with-using).
        using (Document pdfDocument = new Document(xmlFile, loadOptions))
        {
            // Save the resulting PDF.
            pdfDocument.Save(pdfFile);
        }

        Console.WriteLine($"PDF generated successfully: {pdfFile}");
    }
}