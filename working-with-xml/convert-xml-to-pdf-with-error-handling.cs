using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for IWarningCallback, WarningInfo, ReturnAction

namespace PdfGenerationFromXml
{
    // Custom warning handler that implements IWarningCallback.
    // It receives warnings generated during loading and prints a friendly message.
    // Returning ReturnAction.Continue tells the loader to keep processing.
    class CustomWarningHandler : IWarningCallback
    {
        public ReturnAction Warning(WarningInfo warningInfo)
        {
            Console.WriteLine($"[Warning] {warningInfo.WarningMessage}");
            return ReturnAction.Continue;
        }
    }

    class Program
    {
        static void Main()
        {
            const string xmlInputPath  = "input.xml";   // source XML file
            const string pdfOutputPath = "output.pdf";  // destination PDF file

            if (!File.Exists(xmlInputPath))
            {
                Console.Error.WriteLine($"Error: XML file not found – '{xmlInputPath}'.");
                return;
            }

            try
            {
                // Configure load options for XML → PDF conversion.
                // The WarningHandler captures parsing warnings and reports them.
                XmlLoadOptions loadOptions = new XmlLoadOptions
                {
                    WarningHandler = new CustomWarningHandler()
                };

                // Load the XML document with the specified options.
                // Any parsing errors will throw a PdfException which we catch below.
                using (Document pdfDoc = new Document(xmlInputPath, loadOptions))
                {
                    // Save the generated PDF.
                    pdfDoc.Save(pdfOutputPath);
                    Console.WriteLine($"PDF successfully created at '{pdfOutputPath}'.");
                }
            }
            catch (PdfException pdfEx)
            {
                // Handles errors that occur during XML parsing or PDF generation.
                Console.Error.WriteLine($"PDF generation failed: {pdfEx.Message}");
            }
            catch (Exception ex)
            {
                // Fallback for any unexpected exceptions.
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}