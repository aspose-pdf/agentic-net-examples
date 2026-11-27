using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDirectory = "FormXmlOutput";
        const string outputFileName = "formdata.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output subdirectory exists
        Directory.CreateDirectory(outputDirectory);
        string outputPath = Path.Combine(outputDirectory, outputFileName);

        // Use the Form facade (not FormDataConverter) to export form data as XML
        Form formFacade = new Form();
        formFacade.BindPdf(inputPdf);

        // Export the XML using a FileStream opened with FileMode.Create
        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            formFacade.ExportXml(fs);
        }

        Console.WriteLine($"Form data XML saved to '{outputPath}'.");
    }
}
