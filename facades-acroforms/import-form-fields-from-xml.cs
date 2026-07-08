using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "template.pdf";   // PDF with form fields
        const string xmlDataPath   = "data.xml";       // XML containing field values
        const string outputPdfPath = "filled.pdf";     // Resulting PDF

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        try
        {
            // Initialize the Form facade with the source PDF
            using (Form form = new Form(inputPdfPath))
            {
                // Import field values from the XML stream
                using (FileStream xmlStream = new FileStream(xmlDataPath, FileMode.Open, FileAccess.Read))
                {
                    form.ImportXml(xmlStream);
                }

                // Save the updated PDF to the specified output file
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields imported successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}