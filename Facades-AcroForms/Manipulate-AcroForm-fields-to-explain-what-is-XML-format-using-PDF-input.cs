using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXml = "fields.xml";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the Form facade on the loaded document
            Form form = new Form(doc);

            // Export the current AcroForm fields to an XML file
            using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
            {
                form.ExportXml(xmlStream);
            }

            // Fill a text field named "Explanation" with a description of the XML format
            string xmlExplanation = "XML (eXtensible Markup Language) is a text‑based format for representing structured data using tags.";
            bool filled = form.FillField("Explanation", xmlExplanation);
            if (!filled)
            {
                Console.WriteLine("Field 'Explanation' not found; no value was set.");
            }

            // Save the modified PDF document
            form.Save(outputPdf);
        }

        Console.WriteLine("AcroForm fields exported to XML and PDF updated.");
    }
}