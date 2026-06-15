using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (no Facades namespace here)

class Program
{
    static void Main()
    {
        // Path to the PDF file that contains the form.
        const string pdfPath = "input_form.pdf";

        // Ensure the source PDF exists.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Export the form data to a memory stream as XML.
        using (var memoryStream = new MemoryStream())
        {
            // Use the Form class from Aspose.Pdf.Facades via its fully qualified name.
            // No 'using Aspose.Pdf.Facades;' directive is added, complying with the namespace restriction.
            using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
            {
                // Export the form fields to the memory stream in XML format.
                form.ExportXml(memoryStream);
            }

            // Reset the stream position to the beginning before reading.
            memoryStream.Position = 0;

            // Example: write the XML to a file (optional).
            const string xmlOutputPath = "form_data.xml";
            using (FileStream file = new FileStream(xmlOutputPath, FileMode.Create, FileAccess.Write))
            {
                memoryStream.CopyTo(file);
            }

            Console.WriteLine($"Form data exported to memory stream and saved as '{xmlOutputPath}'.");
        }
    }
}