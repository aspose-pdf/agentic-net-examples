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

        try
        {
            // Load the XML file with load options.
            // The WarningHandler will capture any warnings (e.g., malformed XML) and decide whether to continue.
            XmlLoadOptions loadOptions = new XmlLoadOptions
            {
                WarningHandler = new XmlWarningHandler()
            };

            // Create the PDF document from the XML source.
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF.
                pdfDocument.Save(pdfPath);
                Console.WriteLine($"PDF successfully saved to '{pdfPath}'.");
            }
        }
        // Specific exception for conversion problems.
        catch (ConvertException ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
        // General PDF processing errors.
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        // Any other unexpected errors.
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // Implementation of the warning callback interface.
    // Returns Continue to keep processing after a warning, or Abort to stop.
    private class XmlWarningHandler : IWarningCallback
    {
        // The correct method signature for Aspose.Pdf's IWarningCallback.
        public ReturnAction Warning(WarningInfo info)
        {
            // Log the warning details.
            Console.WriteLine($"Warning: {info.WarningMessage}");
            // Continue processing despite the warning.
            return ReturnAction.Continue;
        }
    }
}
