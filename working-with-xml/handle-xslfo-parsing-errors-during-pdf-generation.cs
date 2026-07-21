using System;
using System.IO;
using Aspose.Pdf;

// Custom handler that implements IWarningCallback.
// It receives a WarningInfo object and returns a ReturnAction.
class CustomWarningHandler : IWarningCallback
{
    public ReturnAction Warning(WarningInfo info)
    {
        // Log the warning in a user‑friendly way.
        // The property that contains the warning text is "WarningMessage".
        Console.WriteLine($"[XSL‑FO Warning] {info.WarningMessage}");
        // Continue processing; change to ReturnAction.Abort if you want to stop on a warning.
        return ReturnAction.Continue;
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.xslfo";   // source XSL‑FO file
        const string outputPath = "output.pdf";   // generated PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Configure loading options to handle parsing errors gracefully.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions
        {
            ParsingErrorsHandlingType = XslFoLoadOptions.ParsingErrorsHandlingTypes.InvokeCustomHandler,
            // Assign an instance of the custom IWarningCallback implementation.
            WarningHandler = new CustomWarningHandler()
        };

        try
        {
            // Load the XSL‑FO document with the configured options.
            using (Document pdfDoc = new Document(inputPath, loadOptions))
            {
                // Save the resulting PDF.
                pdfDoc.Save(outputPath);
                Console.WriteLine($"PDF generated successfully: {outputPath}");
            }
        }
        catch (PdfException ex)
        {
            // Handles errors thrown by Aspose.Pdf during loading or saving.
            Console.Error.WriteLine("Failed to generate PDF due to a parsing error:");
            Console.Error.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            // Catch‑all for any unexpected issues.
            Console.Error.WriteLine("An unexpected error occurred:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}
