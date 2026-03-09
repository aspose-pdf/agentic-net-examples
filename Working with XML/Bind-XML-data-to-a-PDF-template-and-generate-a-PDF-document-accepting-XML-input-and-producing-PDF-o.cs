using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <template.pdf> <data.xml> <output.pdf>
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: program <template.pdf> <data.xml> <output.pdf>");
            return;
        }

        string templatePath = args[0];
        string xmlPath      = args[1];
        string outputPath   = args[2];

        // Verify that the input files exist
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template PDF not found: {templatePath}");
            return;
        }

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlPath}");
            return;
        }

        try
        {
            // Load the PDF template
            using (Document pdfDoc = new Document(templatePath))
            {
                // Bind the XML data to the PDF (fills XFA form fields)
                pdfDoc.BindXml(xmlPath);

                // Save the resulting PDF document
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF generated successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}