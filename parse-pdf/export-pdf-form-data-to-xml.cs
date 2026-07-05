using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF containing a form.
        const string pdfPath = "input_form.pdf";

        // Ensure the file exists before proceeding.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (optional – we only need it to verify the form exists).
        using (Document doc = new Document(pdfPath))
        {
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any form fields.");
                return;
            }
        }

        // Export the form data to a memory stream as XML using the Facade Form class.
        using (MemoryStream xmlStream = new MemoryStream())
        {
            // NOTE: We do NOT add a using directive for Aspose.Pdf.Facades as per the namespace restriction.
            // The class is referenced with its fully‑qualified name.
            using (var formFacade = new Aspose.Pdf.Facades.Form())
            {
                formFacade.BindPdf(pdfPath);
                formFacade.ExportXml(xmlStream);
            }

            // Reset the stream position to the beginning for reading.
            xmlStream.Position = 0;

            // Optionally, read the XML as a string (e.g., for display or further processing).
            using (var reader = new StreamReader(xmlStream))
            {
                string xmlContent = reader.ReadToEnd();
                Console.WriteLine("Exported Form XML:");
                Console.WriteLine(xmlContent);
            }

            // If you need the raw bytes elsewhere, you can retrieve them:
            // byte[] xmlBytes = xmlStream.ToArray();
        }
    }
}
