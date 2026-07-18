using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing a form.
        const string inputPdfPath = "form.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Create a memory stream to hold the exported XML (XFDF) data.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Export annotations (including form fields) to XFDF format.
                // XFDF is an XML representation of form data and annotations.
                pdfDocument.ExportAnnotationsToXfdf(xmlStream);

                // Reset the stream position to the beginning for reading.
                xmlStream.Position = 0;

                // Example: read the XML content as a string (optional).
                using (StreamReader reader = new StreamReader(xmlStream))
                {
                    string xmlContent = reader.ReadToEnd();
                    Console.WriteLine("Exported XML (XFDF) content:");
                    Console.WriteLine(xmlContent);
                }

                // At this point, xmlStream contains the XML representation of the form data.
                // It can be returned, saved, or processed further as needed.
            }
        }
    }
}