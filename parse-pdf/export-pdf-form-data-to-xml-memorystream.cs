using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Use the Form class from Aspose.Pdf.Facades via fully qualified name
        // (no using Aspose.Pdf.Facades directive as per the namespace restriction)
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Export the form data as XML directly into the memory stream
                form.ExportXml(xmlStream);

                // Reset the stream position to read the exported XML
                xmlStream.Position = 0;
                string xmlContent = new StreamReader(xmlStream).ReadToEnd();

                Console.WriteLine("Exported Form XML:");
                Console.WriteLine(xmlContent);
            }
        }
    }
}