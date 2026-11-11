using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "formdata.xml";
        const string outputPath = "output.pdf";

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

        // Load the PDF document and bind it to the Form facade.
        using (Document doc = new Document(pdfPath))
        using (Form form = new Form(doc))
        using (FileStream xmlStream = File.OpenRead(xmlPath))
        {
            // Import form field values from the XML file.
            // The Form facade does not expose a PreserveOriginalFieldOrder property;
            // importing via ImportXml keeps the original field order automatically.
            form.ImportXml(xmlStream);

            // Save the updated PDF.
            form.Save(outputPath);
        }

        Console.WriteLine($"Form fields imported and saved to '{outputPath}'.");
    }
}
