using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Pdf;

class Program
{
    // Async entry point (C# 7.1+)
    static async Task Main(string[] args)
    {
        // Input PDF and XML files
        const string pdfPath = "input.pdf";
        const string xmlPath = "formData.xml";
        const string outputPath = "output.pdf";

        // Validate files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Cancellation token to allow graceful cancellation
        using CancellationTokenSource cts = new CancellationTokenSource();

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Load XML form data into an XmlDocument
                XmlDocument xmlDoc = new XmlDocument();
                using (FileStream xmlStream = File.OpenRead(xmlPath))
                {
                    xmlDoc.Load(xmlStream);
                }

                // Assign the XML (XFA) data to the PDF form
                // This imports the form fields values from the XML
                pdfDoc.Form.AssignXfa(xmlDoc);

                // Asynchronously save the updated PDF without blocking the thread
                await pdfDoc.SaveAsync(outputPath, cts.Token);
            }

            Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
        }
        catch (OperationCanceledException)
        {
            Console.Error.WriteLine("Operation was cancelled.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}