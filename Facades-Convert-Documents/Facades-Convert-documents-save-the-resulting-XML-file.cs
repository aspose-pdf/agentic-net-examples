using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and output XML file paths
        string pdfPath = "input.pdf";
        string xmlPath = "output.xml";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        try
        {
            // Create the Form facade and bind the PDF document
            using (Form form = new Form())
            {
                form.BindPdf(pdfPath);

                // Export the form fields to an XML stream
                using (MemoryStream xmlStream = new MemoryStream())
                {
                    form.ExportXml(xmlStream);
                    // Save the XML stream to a file
                    File.WriteAllBytes(xmlPath, xmlStream.ToArray());
                }
            }

            Console.WriteLine($"XML file successfully saved to '{xmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}