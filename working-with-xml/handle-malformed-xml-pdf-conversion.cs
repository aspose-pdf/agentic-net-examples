using System;
using System.IO;
using Aspose.Pdf;

// Custom warning handler that implements IWarningCallback.
// It aborts the loading process when a warning (e.g., malformed XML) occurs.
class MyWarningHandler : IWarningCallback
{
    public ReturnAction Warning(WarningInfo info)
    {
        Console.Error.WriteLine($"Load warning: {info.WarningMessage}");
        return ReturnAction.Abort; // Stop loading on warning.
    }
}

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

        // Configure load options with the custom warning handler.
        XmlLoadOptions loadOptions = new XmlLoadOptions
        {
            WarningHandler = new MyWarningHandler()
        };

        try
        {
            // Load the XML and convert it to PDF.
            using (Document pdfDoc = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(pdfPath);
                Console.WriteLine($"PDF successfully saved to '{pdfPath}'.");
            }
        }
        catch (PdfException ex)
        {
            // Handles errors raised by Aspose.Pdf (e.g., malformed XML).
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected exceptions.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}