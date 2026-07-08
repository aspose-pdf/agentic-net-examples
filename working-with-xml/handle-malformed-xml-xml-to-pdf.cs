using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Custom warning handler that aborts the load operation on any warning (e.g., malformed XML)
    private class AbortOnWarningHandler : IWarningCallback
    {
        // The IWarningCallback interface defines a single method named Warning.
        // Returning ReturnAction.Abort tells Aspose.Pdf to stop processing when a warning occurs.
        public ReturnAction Warning(WarningInfo warningInfo)
        {
            // Optionally log warningInfo.Message here.
            return ReturnAction.Abort;
        }
    }

    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string pdfPath = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Configure load options for XML → PDF conversion
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Abort the load operation if any warning (e.g., malformed XML) is raised
        loadOptions.WarningHandler = new AbortOnWarningHandler();

        try
        {
            // Load the XML file using the configured options
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(pdfPath);
            }

            Console.WriteLine($"Conversion succeeded: '{pdfPath}'");
        }
        catch (PdfException ex) // Handles errors specific to Aspose.Pdf processing
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex) // Handles any other unexpected errors (e.g., malformed XML)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}