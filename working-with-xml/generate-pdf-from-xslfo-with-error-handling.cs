using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ReturnAction enum

class Program
{
    static void Main()
    {
        const string inputXslFo = "input.xslfo";
        const string outputPdf  = "output.pdf";

        if (!File.Exists(inputXslFo))
        {
            Console.Error.WriteLine($"Input file not found: {inputXslFo}");
            return;
        }

        // Configure load options to handle parsing errors gracefully
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Use custom handler so we can log errors and decide whether to continue
        loadOptions.ParsingErrorsHandlingType = XslFoLoadOptions.ParsingErrorsHandlingTypes.InvokeCustomHandler;

        // Assign an implementation of IWarningCallback
        loadOptions.WarningHandler = new CustomWarningHandler();

        try
        {
            // Load the XSL‑FO document with the configured options
            using (Document pdfDoc = new Document(inputXslFo, loadOptions))
            {
                // Save the generated PDF
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}

// Custom implementation of IWarningCallback to handle parsing warnings/errors
class CustomWarningHandler : IWarningCallback
{
    // The IWarningCallback interface defines a single method named Warning.
    // It receives a WarningInfo object and must return a ReturnAction value.
    public ReturnAction Warning(WarningInfo warningInfo)
    {
        // Use the correct property name for the warning message. In recent
        // Aspose.Pdf versions the property is called "WarningMessage".
        string message = warningInfo.WarningMessage;
        Console.WriteLine($"Parsing issue: {message}");
        // Continue processing; return ReturnAction.Abort to stop on first error if desired
        return ReturnAction.Continue;
    }
}