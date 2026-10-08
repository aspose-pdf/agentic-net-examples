using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional: load a license to avoid evaluation limitations.
        try
        {
            // var license = new Aspose.Pdf.License();
            // license.SetLicense("Aspose.Pdf.lic");
        }
        catch (Exception licEx)
        {
            Console.Error.WriteLine($"License loading failed: {licEx.Message}");
            // Continue in evaluation mode.
        }

        // StringWriter will hold the extracted text for logging integration
        using (var stringWriter = new StringWriter())
        {
            // Load the PDF document; using ensures deterministic disposal
            using (var doc = new Document(inputPath))
            {
                try
                {
                    // Extract all text from the document using TextAbsorber (recommended API)
                    var absorber = new TextAbsorber();
                    doc.Pages.Accept(absorber);

                    // Write the extracted text into the StringWriter
                    stringWriter.Write(absorber.Text);
                }
                catch (Exception ex)
                {
                    // This catch specifically handles errors that may occur during extraction.
                    Console.Error.WriteLine($"Error during text extraction: {ex.Message}");
                    Console.Error.WriteLine("Make sure the Aspose.Pdf NuGet package is referenced and that all native files are present in the output directory.");
                    return;
                }
            }

            // The StringWriter now contains the PDF text; it can be passed to any logging framework
            Console.WriteLine("Extracted text:");
            Console.WriteLine(stringWriter.ToString());
        }
    }
}
