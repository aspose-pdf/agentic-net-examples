using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";   // source XML file
        const string pdfPath = "output.pdf";  // destination PDF file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Error: XML file not found – {xmlPath}");
            return;
        }

        // Configure load options for XML → PDF conversion
        XmlLoadOptions loadOptions = new XmlLoadOptions
        {
            // Assign a concrete IWarningCallback implementation
            WarningHandler = new XmlWarningHandler()
        };

        try
        {
            // Load the XML document with the specified options
            using (Document pdfDocument = new Document(xmlPath, loadOptions))
            {
                // Save the resulting PDF
                pdfDocument.Save(pdfPath);
                Console.WriteLine($"PDF successfully created: {pdfPath}");
            }
        }
        // Specific Aspose.Pdf exception for load/save errors
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        // Fallback for any other unexpected errors
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    // ---------------------------------------------------------------------
    // IWarningCallback implementation – handles malformed XML warnings.
    // ---------------------------------------------------------------------
    private class XmlWarningHandler : IWarningCallback
    {
        // The IWarningCallback interface defines a method named "Warning".
        // It receives a WarningInfo object that contains the warning details.
        public ReturnAction Warning(WarningInfo warning)
        {
            // Use the correct property name (WarningMessage) to retrieve the text.
            Console.Error.WriteLine($"Warning during XML load: {warning.WarningMessage}");
            // Abort further processing when a warning occurs to keep the app stable.
            return ReturnAction.Abort;
        }
    }
}
