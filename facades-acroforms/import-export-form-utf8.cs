using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string inputXmlPath = "unicode_input.xml";
        const string outputPdfPath = "output.pdf";
        const string outputXmlPath = "unicode_output.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine("PDF not found: " + inputPdfPath);
            return;
        }
        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine("XML not found: " + inputXmlPath);
            return;
        }

        // Import XML with UTF-8 encoding
        using (Form form = new Form(inputPdfPath, outputPdfPath))
        {
            string xmlText;
            using (StreamReader reader = new StreamReader(inputXmlPath, Encoding.UTF8))
            {
                xmlText = reader.ReadToEnd();
            }
            byte[] xmlBytes = Encoding.UTF8.GetBytes(xmlText);
            using (MemoryStream xmlStream = new MemoryStream(xmlBytes))
            {
                form.ImportXml(xmlStream);
            }

            // Save the PDF after importing Unicode values
            form.Save();
        }

        // Export XML with UTF-8 encoding
        using (Form exportForm = new Form(outputPdfPath))
        {
            using (MemoryStream exportStream = new MemoryStream())
            {
                exportForm.ExportXml(exportStream);
                using (FileStream fileStream = new FileStream(outputXmlPath, FileMode.Create, FileAccess.Write))
                {
                    exportStream.Position = 0;
                    exportStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine("Import and export completed with UTF-8 encoding.");
    }
}
