using System;
using System.IO;
using Aspose.Pdf;

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

        // Configure load options with a warning handler that aborts on malformed XML
        XmlLoadOptions loadOptions = new XmlLoadOptions
        {
            WarningHandler = new XmlWarningHandler()
        };

        try
        {
            // Load the XML and convert to PDF inside a using block for deterministic disposal
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                doc.Save(pdfPath);
                Console.WriteLine($"PDF successfully saved to '{pdfPath}'.");
            }
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing (e.g., malformed XML)
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Implementation of IWarningCallback required because WarningHandler expects an interface, not a delegate.
    private class XmlWarningHandler : IWarningCallback
    {
        public ReturnAction Warning(WarningInfo warningInfo)
        {
            // Log the warning and abort the loading process
            Console.Error.WriteLine($"Warning: {warningInfo.WarningMessage}");
            return ReturnAction.Abort;
        }
    }
}
