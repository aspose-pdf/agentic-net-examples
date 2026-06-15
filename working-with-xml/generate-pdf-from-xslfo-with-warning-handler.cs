using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ReturnAction

class Program
{
    // Custom warning handler that implements IWarningCallback
    private class WarningHandler : IWarningCallback
    {
        // The interface requires a method named "Warning" that returns ReturnAction.
        public ReturnAction Warning(WarningInfo info)
        {
            // Use the correct property name for the warning text.
            Console.WriteLine($"Parsing warning: {info.WarningMessage}");
            // Continue conversion despite the warning
            return ReturnAction.Continue;
        }
    }

    static void Main()
    {
        const string xslFoPath = "input.xslfo";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Error: XSL‑FO file not found – '{xslFoPath}'.");
            return;
        }

        // Configure load options to handle parsing errors gracefully
        XslFoLoadOptions loadOptions = new XslFoLoadOptions
        {
            // Assign the custom handler instance
            WarningHandler = new WarningHandler(),
            // Choose the strategy that invokes the custom handler for formatting errors
            ParsingErrorsHandlingType = XslFoLoadOptions.ParsingErrorsHandlingTypes.InvokeCustomHandler
        };

        try
        {
            // Load the XSL‑FO document with the configured options
            using (Document pdfDocument = new Document(xslFoPath, loadOptions))
            {
                // Save the resulting PDF
                pdfDocument.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated successfully: '{outputPdf}'.");
        }
        catch (PdfException ex)
        {
            // Provide a user‑friendly message for PDF‑related errors
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
