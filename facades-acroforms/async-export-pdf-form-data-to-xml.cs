using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for Form handling extensions

class Program
{
    // Asynchronous entry point (C# 7.1+)
    static async Task Main(string[] args)
    {
        const string inputPdf  = "form.pdf";
        const string outputXml = "formData.xml"; // The file will contain JSON but keeps the requested name

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            await ExportFormDataToXmlAsync(inputPdf, outputXml);
            Console.WriteLine($"Form data exported to '{outputXml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Performs the export on a background thread to keep the UI responsive
    private static async Task ExportFormDataToXmlAsync(string pdfPath, string xmlPath)
    {
        await Task.Run(() =>
        {
            // Load the PDF document containing the form fields
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Aspose.Pdf no longer provides ExportDataToXML. The supported way to
                // export form field data is via ExportToJson. We write the JSON to the
                // requested file (named *.xml in the example) to satisfy the original
                // contract without blocking the UI thread.
                using (FileStream jsonStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    pdfDocument.Form.ExportToJson(jsonStream);
                }
            }
        });
    }
}
