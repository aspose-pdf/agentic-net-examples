using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ReturnAction enum used in warning handling

class Program
{
    // Paths are built at runtime, therefore they must be static readonly, not const.
    private static readonly string dataDir   = @"YOUR_DATA_DIRECTORY";
    private static readonly string xslFoFile = Path.Combine(dataDir, "XSLFO-to-PDF.xslfo");
    private static readonly string pdfFile   = Path.Combine(dataDir, "XSLFO-to-PDF.pdf");

    static void Main()
    {
        if (!File.Exists(xslFoFile))
        {
            Console.Error.WriteLine($"Error: XSL‑FO source file not found – {xslFoFile}");
            return;
        }

        // Configure load options to handle parsing errors gracefully
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Use the custom handler strategy so we can decide what to do on each error
        loadOptions.ParsingErrorsHandlingType = XslFoLoadOptions.ParsingErrorsHandlingTypes.InvokeCustomHandler;

        // Assign a concrete implementation of IWarningCallback (Aspose.Pdf expects this interface, not a delegate)
        loadOptions.WarningHandler = new CustomWarningHandler();

        try
        {
            // Load the XSL‑FO document with the configured options
            using (Document pdfDocument = new Document(xslFoFile, loadOptions))
            {
                // Save the resulting PDF
                pdfDocument.Save(pdfFile);
                Console.WriteLine($"PDF generated successfully: {pdfFile}");
            }
        }
        catch (PdfException ex)
        {
            // Handle errors specific to Aspose.Pdf processing
            Console.Error.WriteLine("An error occurred while converting the XSL‑FO file to PDF.");
            Console.Error.WriteLine($"Details: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Fallback for any other unexpected errors
            Console.Error.WriteLine("An unexpected error occurred.");
            Console.Error.WriteLine($"Details: {ex.Message}");
        }
    }
}

// Custom warning handler that implements Aspose.Pdf.IWarningCallback
class CustomWarningHandler : IWarningCallback
{
    // The IWarningCallback interface defines a method named 'Warning' that receives a WarningInfo object.
    // Implement that method and return a ReturnAction to control the flow.
    public ReturnAction Warning(WarningInfo warningInfo)
    {
        // Use the correct property name for the warning description.
        // In recent Aspose.Pdf versions the property is 'WarningMessage'.
        Console.WriteLine($"[Parsing warning] {warningInfo.WarningMessage}");
        // Continue processing after the warning; return ReturnAction.Abort to stop
        return ReturnAction.Continue;
    }
}