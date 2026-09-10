using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Custom warning handler implementing IWarningCallback
    private class MyWarningHandler : IWarningCallback
    {
        public ReturnAction Warning(WarningInfo info)
        {
            // Provide a user‑friendly message
            Console.WriteLine($"[XSL‑FO Warning] {info.WarningMessage}");
            // Continue processing despite the warning
            return ReturnAction.Continue;
        }
    }

    static void Main()
    {
        const string xslFoPath = "input.xslfo";
        const string pdfPath   = "output.pdf";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Configure load options with custom parsing‑error handling
        var loadOptions = new XslFoLoadOptions
        {
            ParsingErrorsHandlingType = XslFoLoadOptions.ParsingErrorsHandlingTypes.InvokeCustomHandler,
            WarningHandler = new MyWarningHandler()
        };

        try
        {
            // Load the XSL‑FO file and generate a PDF document
            using (Document pdfDoc = new Document(xslFoPath, loadOptions))
            {
                // Save the resulting PDF
                pdfDoc.Save(pdfPath);
                Console.WriteLine($"PDF generated successfully: {pdfPath}");
            }
        }
        catch (PdfException ex)
        {
            // Friendly message for parsing errors that abort conversion
            Console.Error.WriteLine($"Failed to generate PDF: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch‑all for unexpected issues
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}