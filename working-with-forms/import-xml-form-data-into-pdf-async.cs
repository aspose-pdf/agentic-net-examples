using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Aspose.Pdf;

class Program
{
    static async Task Main(string[] args)
    {
        // Paths to the source PDF, XML form data, and the output PDF.
        const string pdfPath = "template.pdf";
        const string xmlPath = "formData.xml";
        const string outputPath = "filled.pdf";

        // Verify that the input files exist.
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the PDF document on a background thread to avoid blocking.
        Document pdfDoc = await Task.Run(() => new Document(pdfPath));

        // Load the XML form data into an XmlDocument.
        XmlDocument xmlDoc = new XmlDocument();
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        {
            xmlDoc.Load(xmlStream);
        }

        // Assign the XML data to the PDF form (XFA). If the PDF does not contain XFA,
        // this call has no effect.
        pdfDoc.Form.AssignXfa(xmlDoc);

        // Save the updated PDF asynchronously.
        await pdfDoc.SaveAsync(outputPath, CancellationToken.None);

        // Release resources.
        pdfDoc.Dispose();

        Console.WriteLine($"PDF with imported XML data saved to '{outputPath}'.");
    }
}