using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string targetPdfPath = "target.pdf";
        const string outputPdfPath = "synchronized.pdf";
        const string tempXmlPath   = "formData.xml";

        // Verify input files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(targetPdfPath))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdfPath}");
            return;
        }

        try
        {
            // Export form data from the source PDF to an XML file
            using (Document sourceDoc = new Document(sourcePdfPath))
            {
                sourceDoc.SaveXml(tempXmlPath);
            }

            // Import the exported XML data into the target PDF
            using (Document targetDoc = new Document(targetPdfPath))
            {
                // BindXml loads the XML representation (including form field values) into the document
                targetDoc.BindXml(tempXmlPath);
                targetDoc.Save(outputPdfPath);
            }

            // Clean up temporary XML file
            if (File.Exists(tempXmlPath))
                File.Delete(tempXmlPath);

            Console.WriteLine($"Form data synchronized successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}