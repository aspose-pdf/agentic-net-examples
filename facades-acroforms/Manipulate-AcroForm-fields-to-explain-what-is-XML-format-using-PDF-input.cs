using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";
        const string outputXml = "fields.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and fill a text field with an explanation of XML format
        using (Form form = new Form(inputPdf))
        {
            string xmlExplanation = "XML (eXtensible Markup Language) is a text‑based format for representing structured data using tags.";
            // Replace "Explanation" with the actual field name in your PDF
            form.FillField("Explanation", xmlExplanation);

            // Save the modified PDF
            form.Save(outputPdf);
        }

        // Export the form fields (including the newly filled field) to an XML file
        using (Form exportForm = new Form(outputPdf))
        using (FileStream xmlStream = new FileStream(outputXml, FileMode.Create, FileAccess.Write))
        {
            exportForm.ExportXml(xmlStream);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
        Console.WriteLine($"Form fields exported to XML file '{outputXml}'.");
    }
}