using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";      // PDF with form fields
        const string outputPdf = "output.pdf";     // Resulting PDF after import
        const string xmlFile   = "fields.xml";     // XML containing field values

        // Verify that required files exist
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdf}");
            return;
        }
        if (!File.Exists(xmlFile))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlFile}");
            return;
        }

        // Create the Form facade, binding the source PDF and specifying the output file.
        // This follows the required lifecycle: creation via constructor, internal loading, later saving.
        Form form = new Form(sourcePdf, outputPdf);

        // Import the XML data. The second argument (ignoreFormTemplateChanges) is set to false
        // to keep the original field order and layout unchanged.
        using (FileStream xmlStream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read))
        {
            form.ImportXml(xmlStream, false);
        }

        // Persist the changes to the output PDF.
        form.Save();

        Console.WriteLine($"Form fields imported from '{xmlFile}' and saved to '{outputPdf}'.");
    }
}