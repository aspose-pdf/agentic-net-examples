using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    // Async entry point to avoid blocking the main thread
    static async Task Main(string[] args)
    {
        const string xmlPath = "formData.xml";      // Path to the XML form data file
        const string outputPdf = "filledForm.pdf"; // Destination PDF file

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load the XML into a PDF document using XmlLoadOptions
        XmlLoadOptions loadOptions = new XmlLoadOptions();

        // Ensure the Document is disposed properly
        using (Document pdfDoc = new Document(xmlPath, loadOptions))
        {
            // Asynchronously save the PDF; this does not block the calling thread
            using (CancellationTokenSource cts = new CancellationTokenSource())
            {
                try
                {
                    await pdfDoc.SaveAsync(outputPdf, cts.Token);
                    Console.WriteLine($"PDF successfully saved to '{outputPdf}'.");
                }
                catch (OperationCanceledException)
                {
                    Console.Error.WriteLine("PDF save operation was canceled.");
                }
            }
        }
    }
}